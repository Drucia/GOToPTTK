package pl.edu.pwr.gotopttk.View.Interfaces;

import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.Trip;

public interface ITripsView {
    void setTripsToVerify(List<Trip> trips);
    void switchProgressBar(boolean isEnabled);
    void displayCommunicate(String s);
}
