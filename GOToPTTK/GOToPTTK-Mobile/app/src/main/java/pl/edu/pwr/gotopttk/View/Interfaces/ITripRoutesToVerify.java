package pl.edu.pwr.gotopttk.View.Interfaces;

import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;

public interface ITripRoutesToVerify {
    void setTripRoutesToVerify(List<TripRoute> trip_routes);

    void switchProgressBar(boolean isEnabled);

    void displayCommunicate(String s);
}
