package pl.edu.pwr.gotopttk.Presenter.Presenters;


import android.support.annotation.NonNull;

import java.util.List;

import pl.edu.pwr.gotopttk.Model.ApiServices.ApiManager;
import pl.edu.pwr.gotopttk.Model.Entities.Route;
import pl.edu.pwr.gotopttk.View.Interfaces.RoutesView;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RouteListPresenter {

    private ApiManager apiManager;
    private RoutesView routesView;

    public RouteListPresenter(RoutesView routesView){
        apiManager = new ApiManager();
        this.routesView = routesView;
    }

    public void giveRoutesToView() {
       apiManager
               .getRoutesService()
               .getRoutes()
               .enqueue(new Callback<List<Route>>() {
                   @Override
                   public void onResponse(@NonNull Call<List<Route>> call, @NonNull Response<List<Route>> response) {
                       List<Route> routes = response.body();
                       if(routes != null){
                           routesView.setRoutes(routes);
                       }
                       else {
                           routesView.displayErrorMessage();
                       }
                   }
                   @Override
                   public void onFailure(@NonNull Call<List<Route>> call, @NonNull Throwable t) {
                        routesView.displayErrorMessage();
                   }
               });
    }

    public void giveMountainRegionsToView() {
        apiManager
                .getMountainRegionsService()
                .getMountainGroupsNames()
                .enqueue(new Callback<List<String>>() {
                    @Override
                    public void onResponse(Call<List<String>> call, Response<List<String>> response) {
                        List<String> mountainRegions = response.body();
                        if(mountainRegions != null){
                            routesView.setMountainGroups(mountainRegions);
                        }
                    }

                    @Override
                    public void onFailure(Call<List<String>> call, Throwable t) {
                        routesView.displayErrorMessage();
                    }
                });
    }
}
