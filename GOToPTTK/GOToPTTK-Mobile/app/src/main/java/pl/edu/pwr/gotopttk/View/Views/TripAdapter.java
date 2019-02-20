package pl.edu.pwr.gotopttk.View.Views;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.Trip;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripDialog;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripsView;

public class TripAdapter extends RecyclerView.Adapter<TripAdapter.ViewHolder> implements ITripsView {

    private List<Trip> trips;
    private ITripDialog listener;

    @Override
    public TripAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.row_trip, parent, false);
        return new TripAdapter.ViewHolder(view);
    }

    public TripAdapter(Context context)
    {
        trips = new ArrayList<>();
        listener = (ITripDialog) context;
    }

    @Override
    public void onBindViewHolder(TripAdapter.ViewHolder holder, int position) {
        holder.bind(trips.get(position), listener, position);
    }

    @Override
    public int getItemCount() {
        return trips.size();
    }

    @Override
    public void setTripsToVerify(List<Trip> trips) {
        this.trips = trips;
        notifyDataSetChanged();
    }

    @Override
    public void switchProgressBar(boolean isEnabled) {
        listener.switchProgressBar(isEnabled);
    }

    @Override
    public void displayCommunicate(String s) {
        listener.displayCommunicate(s);
    }

    class ViewHolder extends RecyclerView.ViewHolder {
        private TextView title;
        private TextView tourist;
        private TextView date;
        private ImageView divider;

        ViewHolder(View itemView) {
            super(itemView);
            title = itemView.findViewById(R.id.title);
            tourist = itemView.findViewById(R.id.tourist);
            date = itemView.findViewById(R.id.date);
            divider = itemView.findViewById(R.id.trip_divider);
        }

        public void bind(Trip trip, ITripDialog listener, int position) {
            title.setText(trip.routes.get(0).route.start.name + " - " + trip.routes.get(trip.routes.size()-1).route.end.name);
            tourist.setText(trip.tourist.user.name + " " + trip.tourist.user.surname);
            SimpleDateFormat input_format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
            SimpleDateFormat output_format = new SimpleDateFormat("dd.MM.yyyy");
            boolean enableButton = trip.guide != null;
            try {
                Date dat = input_format.parse(trip.endDate);
                date.setText("wysłane: " + output_format.format(dat));
            } catch (ParseException e) {
                e.printStackTrace();
            }
            if (position == trips.size()-1)
                divider.setVisibility(View.INVISIBLE);
            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    listener.displayDialogAbout(trip, enableButton);
                }
            });
        }
    }
}
