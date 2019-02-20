package pl.edu.pwr.gotopttk.View.Views;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.Trip;
import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;
import pl.edu.pwr.gotopttk.Presenter.Presenters.TripListPresenter;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripDialog;

public class TripListActivity extends AppCompatActivity implements ITripDialog{
    private TripListPresenter tripPresenter;
    private TripAdapter tripAdapter;
    private String title;
    private ProgressBar progressBar;
    private AlertDialog alertDialog;
    private AlertDialog verifyDialog;
    private final static int GUIDE_ID = 2;
    public final static int VERIFY_CODE = 7;

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_trip_list);
        progressBar = findViewById(R.id.progressBar);
        progressBar.getIndeterminateDrawable().setColorFilter(getResources().getColor(R.color.colorPrimaryDark), android.graphics.PorterDuff.Mode.MULTIPLY);

        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialogTheme);
        builder.setCancelable(false);
        alertDialog = builder.create();
        tripAdapter = new TripAdapter(this);
        tripPresenter = new TripListPresenter(tripAdapter);
        tripPresenter.giveTripsToView(GUIDE_ID);

        RecyclerView recyclerView = findViewById(R.id.recyclerForTrip);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.setHasFixedSize(true);
        recyclerView.setAdapter(tripAdapter);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == VERIFY_CODE && resultCode != Activity.RESULT_CANCELED)
        {
            tripAdapter = new TripAdapter(this);
            tripPresenter = new TripListPresenter(tripAdapter);
            tripPresenter.giveTripsToView(GUIDE_ID);

            RecyclerView recyclerView = findViewById(R.id.recyclerForTrip);
            recyclerView.setLayoutManager(new LinearLayoutManager(this));
            recyclerView.setHasFixedSize(true);
            recyclerView.setAdapter(tripAdapter);
        }
    }

    public void onClickedVerify(Trip trip, boolean isVerifyWith)
    {
        verifyDialog.dismiss();
        Intent intent;
        if (isVerifyWith)
            intent = new Intent(TripListActivity.this, VerifyWithPartInActivity.class);
        else
            intent = new Intent(TripListActivity.this, VerifyWithoutPartInActivity.class);
        intent.putExtra("GUIDE_ID", GUIDE_ID);
        intent.putExtra("TRIP_NAME", title);
        intent.putExtra("TRIP", trip);
        startActivityForResult(intent, VERIFY_CODE);
    }

    @Override
    public void displayDialogAbout(Trip trip, boolean enableButton) {
        String start = trip.routes.get(0).route.start.name;
        String end = trip.routes.get(trip.routes.size()-1).route.end.name;
        title = start + " - " + end;

        final AlertDialog.Builder builder = new AlertDialog.Builder(this);
        LayoutInflater inflater = getLayoutInflater();
        View alertLayout = inflater.inflate(R.layout.verify_dialog, null);
        View titleLayout = inflater.inflate(R.layout.dialog_title, null);
        builder.setView(alertLayout);
        builder.setCustomTitle(titleLayout);
        ((TextView) titleLayout.findViewById(R.id.title_dialog)).setText(title);
        TextView text = alertLayout.findViewById(R.id.state_txt);
        if (trip.state.state.equals("OCZEKUJACA"))
            text.setTextColor(Color.GREEN);
        else
            text.setTextColor(Color.RED);
        text.setText(trip.state.state);
        text = alertLayout.findViewById(R.id.start_date_txt);
        SimpleDateFormat input_format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        SimpleDateFormat output_format = new SimpleDateFormat("dd.MM.yyyy");

        try {
            Date dat = input_format.parse(trip.startDate);
            text.setText(output_format.format(dat));
            dat = input_format.parse(trip.endDate);
            text = alertLayout.findViewById(R.id.end_date_txt);
            text.setText(output_format.format(dat));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        text = alertLayout.findViewById(R.id.guide_txt);

        if (trip.guide == null)
        {
            text.setTextColor(Color.RED);
            text.setText("BRAK");
        } else
            text.setText(trip.guide.user.name + " " + trip.guide.user.surname);

        text = alertLayout.findViewById(R.id.tourist_txt);
        text.setText(trip.tourist.user.name + " " + trip.tourist.user.surname);
        text = alertLayout.findViewById(R.id.points_txt);
        text.setText(trip.points + "");
        text = alertLayout.findViewById(R.id.routes_txt);

        StringBuilder tmp = new StringBuilder();
        List<TripRoute> trip_route = trip.routes;
        for (int i=0; i<trip.routes.size(); i++)
        {
            TripRoute r = trip_route.get(i);
            if (i == 0)
            {
                tmp.append(r.route.start);
                tmp.append(" -> ");
                tmp.append(r.route.end);
            }
            else {
                tmp.append(" -> ");
                tmp.append(r.route.end);
            }
        }

        text.setText(tmp);

        Button but = alertLayout.findViewById(R.id.button_verify_with);
        but.setOnClickListener(view -> this.onClickedVerify(trip, true));
        but.setEnabled(enableButton);
        but = alertLayout.findViewById(R.id.button_verify_without);
        but.setOnClickListener(view -> this.onClickedVerify(trip, false));

        verifyDialog = builder.create();
        verifyDialog.show();
    }

    @Override
    public void switchProgressBar(boolean isEnabled) {
        if(isEnabled)
        {
            alertDialog.show();
            progressBar.setVisibility(View.VISIBLE);
        }
        else
        {
            progressBar.setVisibility(View.GONE);
            alertDialog.dismiss();
        }
    }

    @Override
    public void displayCommunicate(String s) {
        final AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialogTheme);
        builder.setTitle("Komunikat");
        builder.setMessage(s);
        builder.setPositiveButton("OK", (dialogInterface, i) -> finish());
        builder.show();
    }

}
