package pl.edu.pwr.gotopttk.View.Views;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.constraint.ConstraintLayout;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.Fragment;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CompoundButton;
import android.widget.ImageButton;
import android.widget.PopupMenu;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.ToggleButton;

import com.arlib.floatingsearchview.FloatingSearchView;
import com.arlib.floatingsearchview.suggestions.model.SearchSuggestion;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.BitmapDescriptor;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.function.Consumer;
import java.util.stream.Collectors;

import pl.edu.pwr.gotopttk.Model.Entities.Place;
import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;
import pl.edu.pwr.gotopttk.Model.Entities.Route;
import pl.edu.pwr.gotopttk.Presenter.Presenters.MapPresenter;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.PlanningMapView;
import pl.edu.pwr.gotopttk.View.Interfaces.PlanningView;


public class MapFragment extends Fragment implements OnMapReadyCallback, PlanningMapView {


    private static final String START_MARKER_TAG = "START";
    private static final String END_MARKER_TAG = "END";
    private static final int REQUEST_LOCATION_CODE = 1;
    public static final String MARKER_PLACE_TAG = "Place";
    public static final int QUERY_DELAY_MILLIS = 30000;
    public static final int MIN_SUGGESTED =2;
    public static final int ZOOM_LEVEL = 14;


    private MapView mapView;
    private GoogleMap googleMap;
    private MapPresenter mapPresenter;
    private ConstraintLayout planningLayout;
    private FloatingActionButton addButton;
    private TextView startText;
    private TextView endText;
    private Marker startMarker;
    private Marker lastEndMarker;
    private ImageButton planningBackButton;
    private ProgressBar loadingBar;
    private FloatingSearchView searchView;

    private View popupPositionView;

    private List<Place> places;
    private List<Marker> placeMarkers;

    public MapFragment() {
        // Required empty public constructor
    }


    // TODO: Rename and change types and number of parameters
    public static MapFragment newInstance(String param1, String param2) {
        MapFragment fragment = new MapFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        hideActionBar();

        mapPresenter = new MapPresenter(this);
        placeMarkers = new ArrayList<>();
    }

    private void hideActionBar() {
        AppCompatActivity activity = (AppCompatActivity) getActivity();
        activity.getSupportActionBar().hide();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View v = inflater.inflate(R.layout.fragment_map, container, false);
        mapView = v.findViewById(R.id.mapView);
        mapView.onCreate(savedInstanceState);

        //initialize GoogleMap
        mapView.getMapAsync(this);
        mapView.onResume();

        //initialize views
        planningLayout = v.findViewById(R.id.planningLayout);
        addButton = v.findViewById(R.id.floatingAddButton);
        startText = v.findViewById(R.id.start);
        endText = v.findViewById(R.id.end);
        planningBackButton = v.findViewById(R.id.back);
        loadingBar = v.findViewById(R.id.mapLoadingBar);
        searchView = v.findViewById(R.id.floating_search_view);
        popupPositionView = v.findViewById(R.id.popupPositionView);
        ToggleButton searchButton = v.findViewById(R.id.searchButton);

        searchButton.setOnCheckedChangeListener((button, isChecked) -> {
            if(isChecked){
                searchView.setVisibility(View.VISIBLE);
            }
            else {
                searchView.setVisibility(View.GONE);
            }
        });


        searchView.setOnQueryChangeListener((oldQuery, newQuery) -> {
            List<SearchSuggestion> suggestions = suggestPlaces(newQuery);
            if (suggestions.size() > 0) {
                searchView.swapSuggestions(suggestions);
            }
        });


        searchView.setOnSearchListener(new FloatingSearchView.OnSearchListener() {
            @Override
            public void onSuggestionClicked(SearchSuggestion searchSuggestion) {
                searchView.setSearchText(searchSuggestion.getBody());
            }

            @Override
            public void onSearchAction(String currentQuery) {
                if( googleMap != null){
                    Optional<Place> placeOptional = places.stream().filter(p -> p.name.equals(currentQuery)).findAny();
                    if(placeOptional.isPresent()){
                        LatLng latLng = new LatLng(placeOptional.get().latitude, placeOptional.get().longitude);
                        animateMapToPosition(googleMap, latLng);
                    }

                }
            }
        });



        addButton.setOnClickListener(view -> {
            if (startMarker != null && lastEndMarker != null) {
                Place start = createPlaceFromMarker(startMarker);
                Place end = createPlaceFromMarker(lastEndMarker);
                showLoadingBar();
                mapPresenter.createCustomRoute(start, end);
            }
        });

        planningBackButton.setOnClickListener(view -> {
            //clear the map
            removeMarkers();
            //clear labels and hide planning layout
            clearPlanningLayout();
        });

        mapPresenter.getPlaces();

        return v;
    }

    private List<SearchSuggestion> suggestPlaces(String query) {
        if(places != null){
            return places.stream().filter(place -> stringCompare(place.name,query) >= MIN_SUGGESTED).map(PlaceSearchSuggestion::new).collect(Collectors.toList());
        }
        else {
            return new ArrayList<>();
        }
    }

    private static int stringCompare(String s1, String s2){
        int score = 0;
        for (int i = 0; i < s1.length() && i < s2.length() ; i++) {
            if(s1.charAt(i) == s2.charAt(i)){
                score++;
            }
        }
        return score;
    }

    private void showLoadingBar() {
        loadingBar.setVisibility(View.VISIBLE);
    }

    @Override
    public void onResume() {
        super.onResume();
        mapView.onResume();
    }


    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        mapView.onSaveInstanceState(outState);
    }

    @Override
    public void onPause() {
        super.onPause();
        mapView.onPause();
    }

    @Override
    public void onLowMemory() {
        super.onLowMemory();
        mapView.onLowMemory();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        mapView.onDestroy();
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
    }

    @Override
    public void onDetach() {
        super.onDetach();

    }



    @Override
    public void onMapReady(final GoogleMap googleMap) {
        this.googleMap = googleMap;
        googleMap.getUiSettings().setAllGesturesEnabled(true);
        googleMap.setMapType(GoogleMap.MAP_TYPE_TERRAIN);
        if (ActivityCompat.checkSelfPermission(getContext(), Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED &&
                ActivityCompat.checkSelfPermission(getContext(), Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
           ActivityCompat.requestPermissions(getActivity(), new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, REQUEST_LOCATION_CODE);
        }
        else{
            googleMap.setMyLocationEnabled(true);
        }
        if(getPlannedTrip() != null && getPlannedTrip().isEmpty()) {
            googleMap.setOnMapClickListener(point -> {
                //if start is empty show top layout and add a marker
                if (TextUtils.isEmpty(startText.getText())) {
                    removeMarkers();
                    startMarker = createMarker(googleMap, point, START_MARKER_TAG);
                    showPlanningLayout();
                    updateCoordinatesText(startText, point);
                }
                //if start is not empty add a second marker
                else {
                    if (lastEndMarker != null) {
                        lastEndMarker.remove();
                        lastEndMarker = null;
                    }
                    lastEndMarker = createMarker(googleMap, point, END_MARKER_TAG);
                    updateCoordinatesText(endText, point);
                }
            });
            googleMap.setOnMarkerDragListener(new GoogleMap.OnMarkerDragListener() {
                @Override
                public void onMarkerDragStart(Marker marker) {

                }

                @Override
                public void onMarkerDrag(Marker marker) {

                }

                @Override
                public void onMarkerDragEnd(Marker marker) {
                    //move camera to new marker position
                    animateMapToPosition(googleMap, marker.getPosition());
                    if (marker.getTag() instanceof String && marker.getTag().equals(START_MARKER_TAG)) {
                        updateCoordinatesText(startText, marker.getPosition());
                    } else {
                        updateCoordinatesText(endText, marker.getPosition());
                    }
                }
            });


            googleMap.setOnMarkerClickListener(marker -> {
                if (marker.getTag() != null && marker.getTag().equals(MARKER_PLACE_TAG)) {
                    //create popup menu

                    marker.showInfoWindow();
                    PopupMenu menu = MapFragment.this.createPopupMenu(MapFragment.this.getContext(), popupPositionView , marker);
                    animateMapToPosition(googleMap, marker.getPosition(), menu::show);
                    return true;
                }
                return true;
            });
        }

    }

    private PopupMenu createPopupMenu(Context context, View anchor, Marker marker){
        PopupMenu popupMenu = new PopupMenu(context, anchor, Gravity.CENTER_VERTICAL);
        popupMenu.getMenuInflater().inflate(R.menu.marker_popup_menu, popupMenu.getMenu());
        popupMenu.setOnMenuItemClickListener(menuItem -> {
            showPlanningLayout();
            switch (menuItem.getItemId()) {
                case R.id.action_start:
                    startMarker = marker;
                    startText.setText(marker.getTitle());
                    return true;
                case R.id.action_end:
                    lastEndMarker = marker;
                    endText.setText(marker.getTitle());
                    return true;
            }
            return false;
        });
        return popupMenu;
    }


    private void animateMapToPosition(GoogleMap googleMap, LatLng latLng) {
        animateMapToPosition(googleMap, latLng, null);
    }

    private void animateMapToPosition(GoogleMap googleMap, LatLng latLng, Action action) {
        CameraPosition cameraPosition = new CameraPosition.Builder()
                .target(latLng)
                .zoom(ZOOM_LEVEL).build();
        //Zoom in and animate the camera.
        googleMap.animateCamera(CameraUpdateFactory.newCameraPosition(cameraPosition), new GoogleMap.CancelableCallback() {
            @Override
            public void onFinish() {
                if(action != null){
                    action.invoke();
                }
            }

            @Override
            public void onCancel() {

            }
        });
    }


    private void removeMarkers() {
        if(startMarker != null && startMarker.getTag() != MARKER_PLACE_TAG){
            startMarker.remove();
        }
        if(lastEndMarker != null && lastEndMarker.getTag() != MARKER_PLACE_TAG){
            lastEndMarker.remove();
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults){
        switch (requestCode) {
            case REQUEST_LOCATION_CODE: {
                // If request is cancelled, the result arrays are empty.
                if (grantResults.length > 0
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    googleMap.setMyLocationEnabled(true);
                }
                return;

            }
        }
    }

    private Place createPlaceFromMarker(Marker marker){
        return new Place(null, marker.getPosition().longitude, marker.getPosition().latitude, -1);
    }


    private void clearPlanningLayout(){
        startText.setText(null);
        endText.setText(null);
        planningLayout.setVisibility(View.GONE);
        searchView.setVisibility(View.VISIBLE);
    }

    private void updateCoordinatesText(TextView textView, LatLng position){
        textView.setText(coordinatesToString(position));
    }

    private Marker createMarker(GoogleMap googleMap, LatLng position, String tag){
        Marker marker = googleMap.addMarker(new MarkerOptions().position(position));
        marker.setDraggable(true);
        marker.setTag(tag);
        return marker;
    }

    private void showPlanningLayout(){
        if(planningLayout.getVisibility() != View.VISIBLE){
            searchView.setVisibility(View.GONE);
        }
        planningLayout.setVisibility(View.VISIBLE);
        addButton.setVisibility(View.VISIBLE);
    }

    private static String coordinatesToString(LatLng position){
        return String.format("%.7f, %.7f", position.latitude, position.longitude);
    }



    private PlannedTrip getPlannedTrip(){
        PlanningView planningView = (PlanningView) getActivity();
        return planningView.getPlannedTrip();
    }

    @Override
    public void openTripView(List<Route> routes) {
        Intent planningIntent = new Intent(getActivity(), CreateTripActivity.class);
        getPlannedTrip().getRoutes().addAll(routes);
        planningIntent.putExtra(getString(R.string.EXTRA_PLANNED_TRIP), getPlannedTrip());
        startActivity(planningIntent);
    }


    private BitmapDescriptor bitmapDescriptorFromVector(Context context, int vectorResId) {
        Drawable vectorDrawable = ContextCompat.getDrawable(context, vectorResId);
        vectorDrawable.setBounds(0, 0, vectorDrawable.getIntrinsicWidth(), vectorDrawable.getIntrinsicHeight());
        Bitmap bitmap = Bitmap.createBitmap(vectorDrawable.getIntrinsicWidth(), vectorDrawable.getIntrinsicHeight(), Bitmap.Config.ARGB_8888);
        Canvas canvas = new Canvas(bitmap);
        vectorDrawable.draw(canvas);
        return BitmapDescriptorFactory.fromBitmap(bitmap);
    }


    @Override
    public void displayTripCreationErrorMessage() {
        loadingBar.setVisibility(View.GONE);
        ErrorDialogFragment dialog =  ErrorDialogFragment.newInstance (getString(R.string.DIALOG_UNABLE_TO_CREATE_ROUTE), getString(R.string.DIALOG_DEFAULT_TITLE), getString(R.string.DIALOG_DISMISS));
        dialog.show(getFragmentManager(), getString(R.string.DIALOG_ERROR_TAG));
    }

    @Override
    public void displayConnectionErrorMessage() {

        if(getActivity() != null && this.isResumed()) {
            loadingBar.setVisibility(View.GONE);
            ErrorDialogFragment dialog = ErrorDialogFragment.newInstance(getString(R.string.DIALOG_CONNECTION_ERROR_MESSAGE), getString(R.string.DIALOG_DEFAULT_TITLE), getString(R.string.DIALOG_DISMISS));
            dialog.show(getActivity().getSupportFragmentManager(), getString(R.string.DIALOG_ERROR_TAG));

            //try again in 15 seconds
            planningLayout.postDelayed(() -> mapPresenter.getPlaces(), QUERY_DELAY_MILLIS);
        }
    }


    @Override
    public void displayPlaces(List<Place> places) {
        this.places = places;
        places.forEach(p -> createPlaceMarker(googleMap, p));
    }

    private void createPlaceMarker(GoogleMap googleMap, Place place){
        LatLng position = new LatLng(place.latitude, place.longitude);
        Marker marker = googleMap.addMarker(new MarkerOptions().position(position));
        marker.setIcon(bitmapDescriptorFromVector(getContext(), R.drawable.ic_terrain_black_24dp));
        marker.setTitle(place.name);
        marker.setTag(MARKER_PLACE_TAG);

        placeMarkers.add(marker);
    }

    private interface Action { void invoke(); }
}
