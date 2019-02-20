package pl.edu.pwr.gotopttk.View.Views;

import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;
import pl.edu.pwr.gotopttk.Model.Entities.Route;
import pl.edu.pwr.gotopttk.Presenter.Presenters.PlanningPresenter;
import pl.edu.pwr.gotopttk.Presenter.Presenters.RouteListPresenter;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.PlanningView;
import pl.edu.pwr.gotopttk.View.Interfaces.RoutesView;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link RouteListFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link RouteListFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class RouteListFragment extends Fragment  implements RoutesView {

    private RouteListPresenter presenter;
    private PlanningPresenter planningPresenter;
    private RecyclerView recyclerView;
    private RouteAdapter routeAdapter;
    private List<String> mountainGroups;
    private Spinner spinner;
    private TextView emptyListTextView;
    private SwipeRefreshLayout refreshLayout;

    public RouteListFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment RouteListFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static RouteListFragment newInstance(String param1, String param2) {
        RouteListFragment fragment = new RouteListFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        planningPresenter = new PlanningPresenter((PlanningView) getActivity());
        routeAdapter = new RouteAdapter(planningPresenter, getContext());
        presenter = new RouteListPresenter(this);
        mountainGroups = new ArrayList<>();

        getContentFromPresenter();



    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the refreshLayout for this fragment
        return inflater.inflate(R.layout.fragment_route_list, container, false);
    }


    private void getContentFromPresenter(){
        presenter.giveRoutesToView();
        presenter.giveMountainRegionsToView();
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        recyclerView = getView().findViewById(R.id.routeRecyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        recyclerView.setHasFixedSize(true);
        recyclerView.setAdapter(routeAdapter);

        emptyListTextView = view.findViewById(R.id.emptyListLabel);

        spinner = view.findViewById(R.id.mountainRegionSpinner);

        //try to get content on refresh

        refreshLayout = view.findViewById(R.id.swipeRefreshLayout);
        refreshLayout.setOnRefreshListener(this::getContentFromPresenter);

        setContentVisible();
    }



    @Override
    public void onAttach(Context context) {
        super.onAttach(context);

    }

    @Override
    public void onDetach() {
        super.onDetach();
    }


    private PlannedTrip getPlannedTrip(){
        PlanningView planningView = (PlanningView) getActivity();
        return planningView.getPlannedTrip();
    }

    @Override
    public void setRoutes(List<Route> routes){
        PlannedTrip plannedTrip = getPlannedTrip();
        if(plannedTrip != null && !plannedTrip.isEmpty()){
            routes = routes.stream().filter(plannedTrip::isRouteConnectedToTrip).collect(Collectors.toList());
        }
        setContentVisible();
        routeAdapter.setRoutes(routes);
        routeAdapter.notifyDataSetChanged();
    }



    private void setContentVisible(){
        refreshLayout.setRefreshing(false);
        spinner.setVisibility(View.VISIBLE);
        recyclerView.setVisibility(View.VISIBLE);
        emptyListTextView.setVisibility(View.GONE);
    }

    private void hideContent(){
        refreshLayout.setRefreshing(false);
        spinner.setVisibility(View.GONE);
        recyclerView.setVisibility(View.GONE);
        emptyListTextView.setVisibility(View.VISIBLE);
    }



    @Override
    public void setMountainGroups(List<String> mountainGroups) {
        this.mountainGroups = mountainGroups;
        ArrayAdapter<String> adapter = new ArrayAdapter<>(getContext(), R.layout.support_simple_spinner_dropdown_item, mountainGroups);
        // Specify the refreshLayout to use when the list of choices appears
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        // Apply the routeAdapter to the spinner
        spinner.setAdapter(adapter);
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int pos, long id) {
                if(mountainGroups.size() > pos){
                    String mountainGroup = mountainGroups.get(pos);
                    routeAdapter.filter(mountainGroup);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        setContentVisible();
    }

    @Override
    public void displayErrorMessage() {
        hideContent();
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
