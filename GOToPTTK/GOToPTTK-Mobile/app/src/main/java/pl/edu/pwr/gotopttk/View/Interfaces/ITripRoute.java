package pl.edu.pwr.gotopttk.View.Interfaces;

import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;

public interface ITripRoute {
    void switchProgressBar(boolean isEnabled);
    void displayInfoAbout(TripRoute trip_route);
    void displayCommunicate(String s);
}
