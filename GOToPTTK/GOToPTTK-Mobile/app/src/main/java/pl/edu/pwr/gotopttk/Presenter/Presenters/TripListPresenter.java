package pl.edu.pwr.gotopttk.Presenter.Presenters;

import android.support.annotation.NonNull;

import java.util.ArrayList;
import java.util.List;

import pl.edu.pwr.gotopttk.Model.ApiServices.ApiManager;
import pl.edu.pwr.gotopttk.Model.Entities.Trip;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripsView;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TripListPresenter {
    private ApiManager apiManager;
    private ITripsView tripsView;

    public TripListPresenter(ITripsView tripsView){
        apiManager = new ApiManager();
        this.tripsView = tripsView;
    }

    public void giveTripsToView(int gId) {
        tripsView.switchProgressBar(true);
        apiManager
                .getTripListService()
                .getTripsToVerify(gId)
                .enqueue(new Callback<List<Trip>>() {
                    @Override
                    public void onResponse(@NonNull Call<List<Trip>> call, @NonNull Response<List<Trip>> response) {
                        List<Trip> tripsToVerify = response.body();
                        if(tripsToVerify != null && tripsToVerify.size() != 0){
                            tripsView.setTripsToVerify(tripsToVerify);
                            tripsView.switchProgressBar(false);
                        }
                        else
                        {
                            tripsView.setTripsToVerify(new ArrayList<>());
                            tripsView.switchProgressBar(false);
                            tripsView.displayCommunicate("Nie masz wycieczek do zweryfikowania.");
                        }
                    }
                    @Override
                    public void onFailure(@NonNull Call<List<Trip>> call, @NonNull Throwable t) {
                        tripsView.setTripsToVerify(new ArrayList<Trip>());
                        tripsView.switchProgressBar(false);
                        tripsView.displayCommunicate("Ooops... Błąd połączenia z serwerem. Spróbuj ponownie za chwilę :)");
                    }
                });
    }
}
