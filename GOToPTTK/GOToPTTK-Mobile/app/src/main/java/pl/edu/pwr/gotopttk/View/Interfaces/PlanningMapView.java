package pl.edu.pwr.gotopttk.View.Interfaces;


import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.Place;
import pl.edu.pwr.gotopttk.Model.Entities.Route;

public interface PlanningMapView {
    void openTripView(List<Route> routes);

    void displayPlaces(List<Place> places);

    void displayTripCreationErrorMessage();

    void displayConnectionErrorMessage();

}
