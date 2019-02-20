package pl.edu.pwr.gotopttk.View.Interfaces;


import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.Route;

public interface RoutesView {
    void setRoutes(List<Route> routes);
    void setMountainGroups(List<String> mountainGroups);

    void displayErrorMessage();
}
