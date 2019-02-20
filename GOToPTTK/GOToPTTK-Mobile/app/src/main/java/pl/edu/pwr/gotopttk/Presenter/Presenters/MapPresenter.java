package pl.edu.pwr.gotopttk.Presenter.Presenters;


import android.support.annotation.NonNull;

import java.util.List;
import java.util.Locale;

import pl.edu.pwr.gotopttk.Model.ApiServices.ApiManager;
import pl.edu.pwr.gotopttk.Model.ApiServices.OpenElevationApiManager;
import pl.edu.pwr.gotopttk.Model.Entities.CustomRouteRequest;
import pl.edu.pwr.gotopttk.Model.Entities.OpenElevationResult;
import pl.edu.pwr.gotopttk.Model.Entities.Place;
import pl.edu.pwr.gotopttk.Model.Entities.Route;
import pl.edu.pwr.gotopttk.View.Interfaces.PlanningMapView;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MapPresenter {
    private PlanningMapView mapView;
    private ApiManager apiManager;
    private OpenElevationApiManager elevationApiManager;

    public MapPresenter(PlanningMapView mapView) {
        this.mapView = mapView;
        apiManager = new ApiManager();
        elevationApiManager = new OpenElevationApiManager();
    }


    public void getPlaces(){
        apiManager.getPlacesService().getPlaces(true).enqueue(new Callback<List<Place>>() {
            @Override
            public void onResponse(Call<List<Place>> call, Response<List<Place>> response) {
                List<Place> places = response.body();
                if(places != null){
                    mapView.displayPlaces(places);
                }
            }

            @Override
            public void onFailure(Call<List<Place>> call, Throwable t) {
                mapView.displayConnectionErrorMessage();
            }
        });
    }

    public void createCustomRoute(Place start, Place end){
        String locations = String.format(Locale.US, OpenElevationApiManager.LOCATIONS_FORMAT, start.latitude, start.longitude, end.latitude, end.longitude);
        //get elevation of locations
//        elevationApiManager
//                .getElevationService()
//                .getElevationResult(locations)
//                .enqueue(new Callback<OpenElevationResult>() {
//                    @Override
//                    public void onResponse(Call<OpenElevationResult> call, Response<OpenElevationResult> response) {
//                        OpenElevationResult result = response.body();
//                        if(result != null){
//                            start.altitude = result.results.get(0).elevation;
//                            end.altitude = result.results.get(1).elevation;
//                            finishRequest(start, end);
//                        }
//                    }
//
//                    @Override
//                    public void onFailure(Call<OpenElevationResult> call, Throwable t) {
//                        mapView.displayErrorMessage();
//                    }
//                });
        finishRequest(start, end);
    }

    private void finishRequest(Place start, Place end){
        CustomRouteRequest request = new CustomRouteRequest(start, end);
        apiManager
                .getRoutesService()
                .createCustomRoute(request)
                .enqueue(new Callback<List<Route>>() {
                    @Override
                    public void onResponse(@NonNull Call<List<Route>> call, @NonNull Response<List<Route>> response) {
                        List<Route> routes = response.body();
                        if(routes != null){
                            mapView.openTripView(routes);
                        }
                        else{
                            mapView.displayTripCreationErrorMessage();
                        }
                    }

                    @Override
                    public void onFailure(@NonNull Call<List<Route>> call, @NonNull Throwable t) {
                        mapView.displayConnectionErrorMessage();
                    }
                });
    }
}
