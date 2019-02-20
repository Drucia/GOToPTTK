package pl.edu.pwr.gotopttk.View.Interfaces;

import pl.edu.pwr.gotopttk.Model.Entities.Trip;

public interface ITripDialog {
    void displayDialogAbout(Trip trip, boolean enableButton);
    void switchProgressBar(boolean isEnabled);
    void displayCommunicate(String s);
}
