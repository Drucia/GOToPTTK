package pl.edu.pwr.gotopttk.View.Views;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripRoute;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripRoutesToVerify;

public class TripRoutesAdapter extends RecyclerView.Adapter<TripRoutesAdapter.ViewHolder> implements ITripRoutesToVerify {
    List<TripRoute> trip_routes;
    ITripRoute listener;
    int operation_id;

    public TripRoutesAdapter(Context context, int operation_id)
    {
      trip_routes = new ArrayList<>();
      listener = (ITripRoute) context;
      this.operation_id = operation_id;
    }

    @Override
    public TripRoutesAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.row_trip_route_verify, parent, false);
        return new TripRoutesAdapter.ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(TripRoutesAdapter.ViewHolder holder, int position) {
        holder.bind(trip_routes.get(position), listener, position);
    }

    @Override
    public int getItemCount() {
        return trip_routes.size();
    }

    @Override
    public void setTripRoutesToVerify(List<TripRoute> trip_routes) {
        if (operation_id == VerifyWithoutPartInActivity.VERIFY_WITHOUT_ID)
        {
            for (TripRoute t: trip_routes)
            {
                if (t.state.state.equals("OCZEKUJACA"))
                    this.trip_routes.add(t);
            }
        }
        if(operation_id == VerifyWithPartInActivity.VERIFY_WITH_ID)
            this.trip_routes = trip_routes;
        if (this.trip_routes.size() == 0) displayCommunicate("Nie ma odcinków zgodnych z uprawnieniami. Spróbuj weryfikację z udziałem :)");
        notifyDataSetChanged();
    }

    @Override
    public void switchProgressBar(boolean isEnabled)
    {
        listener.switchProgressBar(isEnabled);
    }

    @Override
    public void displayCommunicate(String s) {
        listener.displayCommunicate(s);
    }

    class ViewHolder extends RecyclerView.ViewHolder {
        private TextView route;
        private ImageView done;
        private ImageView divider;

        ViewHolder(View itemView) {
            super(itemView);
            route = itemView.findViewById(R.id.title_route_txt);
            done = itemView.findViewById(R.id.image_done);
            divider = itemView.findViewById(R.id.route_divider);
        }

        public void bind(TripRoute trip_route, ITripRoute listener, int position) {
            route.setText(trip_route.route.start.name + " - " + trip_route.route.end.name);
            if (!trip_route.state.state.equals("ZATWIERDZONA"))
                done.setVisibility(View.GONE);
            if (position == trip_routes.size()-1)
                divider.setVisibility(View.INVISIBLE);
            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    listener.displayInfoAbout(trip_route);
                }
            });
        }
    }
}
