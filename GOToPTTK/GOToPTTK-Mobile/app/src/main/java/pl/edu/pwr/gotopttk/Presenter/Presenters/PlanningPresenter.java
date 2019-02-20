package pl.edu.pwr.gotopttk.Presenter.Presenters;


import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import pl.edu.pwr.gotopttk.Model.ApiServices.ApiManager;
import pl.edu.pwr.gotopttk.Model.ApiServices.RoutesService;
import pl.edu.pwr.gotopttk.Model.Entities.Route;
import pl.edu.pwr.gotopttk.View.Interfaces.PlanningView;
import retrofit2.Response;

public class PlanningPresenter {

    private PlanningView view;


    public PlanningPresenter(PlanningView view) {
        this.view = view;

    }

    public void addRouteToTrip(Route route){
        view.openTripView(route);
    }


}
