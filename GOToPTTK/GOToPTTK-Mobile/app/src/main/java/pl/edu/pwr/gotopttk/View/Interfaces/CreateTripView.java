package pl.edu.pwr.gotopttk.View.Interfaces;


import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;

public interface CreateTripView {

    void openFinishedTripView(PlannedTrip trip);

    void displayErrorMessage();
}
