package pl.edu.pwr.gotopttk.View.Views;


import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

import pl.edu.pwr.gotopttk.Model.Entities.Route;
import pl.edu.pwr.gotopttk.Presenter.Presenters.PlanningPresenter;
import pl.edu.pwr.gotopttk.R;

public class RouteAdapter extends RecyclerView.Adapter<RouteAdapter.ViewHolder>{

    private List<Route> routes = new ArrayList<>();
    private List<Route> filteredRoutes = new ArrayList<>();
    private PlanningPresenter planningPresenter;
    private Context context;

    public RouteAdapter(PlanningPresenter planningPresenter, Context context) {
        this.planningPresenter = planningPresenter;
        this.context = context;
    }


    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.row_route, parent, false);
        return new ViewHolder(view);
    }


    @Override
    public void onBindViewHolder(ViewHolder holder, int position) {
        View rowView = holder.getRowView();
        Route route = filteredRoutes.get(position);
        TextView start = rowView.findViewById(R.id.routeStart);
        start.setText(route.start.name);
        TextView end = rowView.findViewById(R.id.routeEnd);
        end.setText(route.end.name);
        TextView pointsView = rowView.findViewById(R.id.routePoints);
        String points = holder.getRowView().getResources().getString(R.string.pointsFormat, route.points);
        pointsView.setText(points);

        ImageButton button  = rowView.findViewById(R.id.addRoute);
        button.setOnClickListener(view -> planningPresenter.addRouteToTrip(route));
    }

    @Override
    public int getItemCount() {
        return filteredRoutes.size();
    }


    void setRoutes(List<Route> routes) {
        this.routes = routes;
        this.filteredRoutes = routes;
        notifyDataSetChanged();
    }

    void filter(String mountainGroup){
        filteredRoutes = routes.stream().filter(r -> Objects.equals(r.mountainGroup, mountainGroup)).collect(Collectors.toList());
        notifyDataSetChanged();
    }




    class ViewHolder extends RecyclerView.ViewHolder {
        private View rowView;

        ViewHolder(View itemView) {
            super(itemView);
            rowView = itemView;
        }

        View getRowView() {
            return rowView;
        }
    }
}
