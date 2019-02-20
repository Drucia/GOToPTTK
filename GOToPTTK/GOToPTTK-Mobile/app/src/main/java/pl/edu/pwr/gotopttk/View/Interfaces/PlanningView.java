package pl.edu.pwr.gotopttk.View.Interfaces;


import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;
import pl.edu.pwr.gotopttk.Model.Entities.Route;

public interface PlanningView {

    void openTripView(Route route);
    void displayPlanningError();
    PlannedTrip getPlannedTrip();
}
