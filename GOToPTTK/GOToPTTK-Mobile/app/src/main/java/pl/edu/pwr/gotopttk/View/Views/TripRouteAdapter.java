package pl.edu.pwr.gotopttk.View.Views;


import android.content.Context;
import android.support.constraint.ConstraintLayout;
import android.support.v7.widget.RecyclerView;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.ToggleButton;

import java.util.ArrayList;
import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.Route;
import pl.edu.pwr.gotopttk.R;

public class TripRouteAdapter extends RecyclerView.Adapter<TripRouteAdapter.ViewHolder>  {

    private List<Route> routes;
    private Context context;

    public TripRouteAdapter(Context context){
        this.context = context;
        routes = new ArrayList<>();
    }


    public void setRoutes(List<Route> routes) {
        this.routes = routes;
    }


    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.row_trip_route, parent, false);
        return new ViewHolder(view);
    }


    @Override
    public void onBindViewHolder(ViewHolder holder, int position) {
        View rowView = holder.rowView;
        Route route = routes.get(position);

        TextView routeText =  rowView.findViewById(R.id.startEndLabel);
        routeText.setText(route.toString());

        final TextView pointsText = rowView.findViewById(R.id.pointsLabel);
        pointsText.setText( context.getString(R.string.pointsFormat,  route.points));

        ToggleButton toggleButton = rowView.findViewById(R.id.expandButton);
        toggleButton.setOnCheckedChangeListener((button, isChecked) -> {
            ConstraintLayout layout = rowView.findViewById(R.id.tripRouteLayout);
            if(isChecked){
                pointsText.setVisibility(View.GONE);
                setLayoutHeight(layout, android.R.attr.listPreferredItemHeightSmall);
            }
            else {
                pointsText.setVisibility(View.VISIBLE);
                setLayoutHeight(layout, android.R.attr.listPreferredItemHeightLarge);
            }
        });

    }


    private void setLayoutHeight(ConstraintLayout layout, int heightAttr){
        ViewGroup.LayoutParams layoutParams = layout.getLayoutParams();
        TypedValue typedValue = new TypedValue();
        context.getTheme().resolveAttribute(heightAttr, typedValue, true);
        layoutParams.height = TypedValue.complexToDimensionPixelSize(typedValue.data, context.getResources().getDisplayMetrics());
    }


    @Override
    public int getItemCount() {
        return routes.size();
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
