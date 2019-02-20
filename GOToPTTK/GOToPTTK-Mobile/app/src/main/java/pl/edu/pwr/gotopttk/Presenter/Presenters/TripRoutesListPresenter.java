package pl.edu.pwr.gotopttk.Presenter.Presenters;

import android.support.annotation.NonNull;

import java.util.ArrayList;
import java.util.List;

import pl.edu.pwr.gotopttk.Model.ApiServices.ApiManager;
import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripRoutesToVerify;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TripRoutesListPresenter {
    private ApiManager apiManager;
    private ITripRoutesToVerify tripRoutesView;

    public TripRoutesListPresenter(ITripRoutesToVerify tripRoutesView){
        apiManager = new ApiManager();
        this.tripRoutesView = tripRoutesView;
    }

    public void giveTripRoutesToView(int gId, int tId) {
        tripRoutesView.switchProgressBar(true);
        apiManager
                .getTripRoutesService()
                .getTripRoutesToVerify(gId, tId)
                .enqueue(new Callback<List<TripRoute>>() {
                    @Override
                    public void onResponse(@NonNull Call<List<TripRoute>> call, @NonNull Response<List<TripRoute>> response) {
                        List<TripRoute> tripsToVerify = response.body();
                        if(tripsToVerify != null){
                            tripRoutesView.setTripRoutesToVerify(tripsToVerify);
                            tripRoutesView.switchProgressBar(false);
                        }
                    }
                    @Override
                    public void onFailure(@NonNull Call<List<TripRoute>> call, @NonNull Throwable t) {
                        tripRoutesView.setTripRoutesToVerify(new ArrayList<TripRoute>());
                        tripRoutesView.switchProgressBar(false);
                        tripRoutesView.displayCommunicate("Ooops... Błąd połączenia z serwerem. Spróbuj ponownie za chwilę :)");
                    }
                });
    }
}
