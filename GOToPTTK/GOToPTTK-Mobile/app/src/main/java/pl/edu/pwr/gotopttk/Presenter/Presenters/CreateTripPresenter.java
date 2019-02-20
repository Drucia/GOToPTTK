package pl.edu.pwr.gotopttk.Presenter.Presenters;


import pl.edu.pwr.gotopttk.Model.ApiServices.ApiManager;
import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;
import pl.edu.pwr.gotopttk.View.Interfaces.CreateTripView;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreateTripPresenter {

    private ApiManager apiManager;
    private CreateTripView tripView;

    public CreateTripPresenter(CreateTripView tripView) {
        this.apiManager = new ApiManager();
        this.tripView = tripView;
    }

    public void createTrip(PlannedTrip plannedTrip){
        if(plannedTrip != null){
            apiManager.getTripService().createNewTrip(plannedTrip).enqueue(new Callback<PlannedTrip>() {
                @Override
                public void onResponse(Call<PlannedTrip> call, Response<PlannedTrip> response) {
                    PlannedTrip trip = response.body();
                    if(trip != null){
                        tripView.openFinishedTripView(trip);
                    }
                    else{
                        tripView.displayErrorMessage();
                    }
                }

                @Override
                public void onFailure(Call<PlannedTrip> call, Throwable t) {
                    tripView.displayErrorMessage();
                }
            });
        }
    }
}
