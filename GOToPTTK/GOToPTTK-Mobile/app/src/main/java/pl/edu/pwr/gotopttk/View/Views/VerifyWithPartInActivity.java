package pl.edu.pwr.gotopttk.View.Views;

import android.app.ActionBar;
import android.app.Activity;
import android.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import pl.edu.pwr.gotopttk.Model.Entities.Trip;
import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;
import pl.edu.pwr.gotopttk.Model.Entities.VerifyRequest;
import pl.edu.pwr.gotopttk.Presenter.Presenters.TripRoutesListPresenter;
import pl.edu.pwr.gotopttk.Presenter.Presenters.VerifyPresenter;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripRoute;
import pl.edu.pwr.gotopttk.View.Interfaces.IVerifyView;

public class VerifyWithPartInActivity extends AppCompatActivity implements ITripRoute, IVerifyView {
    VerifyPresenter presenter;
    VerifyRequest request;
    private TripRoutesListPresenter tripRoutesPresenter;
    private TripRoutesAdapter tripRoutesAdapter;
    private int guide_id;
    private Trip trip;
    public final static int VERIFY_WITH_ID = 1;
    Date date;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_verify_without_part_in);

        Toast.makeText(this,"Wybrano weryfikację z udziałem", Toast.LENGTH_SHORT).show();

        Bundle extras = getIntent().getExtras();

        guide_id = extras.getInt("GUIDE_ID");
        trip = extras.getParcelable("TRIP");
        String activity_name = extras.getString("TRIP_NAME");
        setTitle(activity_name);
        tripRoutesAdapter = new TripRoutesAdapter(this, VERIFY_WITH_ID);
        tripRoutesPresenter = new TripRoutesListPresenter(tripRoutesAdapter);
        tripRoutesPresenter.giveTripRoutesToView(guide_id, trip.id);

        RecyclerView recycler = findViewById(R.id.recyclerForTripRoutes);
        recycler.setLayoutManager(new LinearLayoutManager(this));
        recycler.setHasFixedSize(true);
        recycler.setAdapter(tripRoutesAdapter);

        SimpleDateFormat input_format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");

        try {
            date = input_format.parse(trip.endDate);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        displayDialogForVerify(activity_name);

        presenter = new VerifyPresenter(this);
    }

    @Override
    public void switchProgressBar(boolean isEnabled) {

    }

    public void displayDialogForVerify(String title)
    {
        final AlertDialog.Builder builder = new AlertDialog.Builder(this);
        LayoutInflater inflater = getLayoutInflater();
        View alertLayout = inflater.inflate(R.layout.verify_dialog_only_buttons, null);
        View titleLayout = inflater.inflate(R.layout.dialog_title, null);
        builder.setView(alertLayout);
        builder.setCustomTitle(titleLayout);
        ((TextView) titleLayout.findViewById(R.id.title_dialog)).setText(title);

        Button but = alertLayout.findViewById(R.id.button_accept);
        but.setOnClickListener(view -> this.onClickedAccept());
        but = alertLayout.findViewById(R.id.button_discard);
        but.setOnClickListener(view -> this.onClickedDiscard());
        //builder.setCancelable(false);
        builder.setOnCancelListener(dialogInterface -> finish());
        builder.show();
    }

    //TODO
    private void onClickedDiscard() {
        // in the future dialog with write issue option :)
        request = new VerifyRequest(date, "", trip.id, guide_id, -1, "ODRZUCONA");
        presenter.createVerify(request);
        setResult(RouteInfoActivity.RESULT_DISCARD);
    }

    private void onClickedAccept() {
        request = new VerifyRequest(date, "", trip.id, guide_id, -1, "ZATWIERDZONA");
        presenter.createVerify(request);
        setResult(RouteInfoActivity.RESULT_APPLY);
    }

    public void displayToast(int optionId) {
        if (optionId == VerifyPresenter.OK_REQUEST)
            displayCommunicate("Poprawnie zweryfikowano wycieczkę");
        else
            displayCommunicate("Brak uprawnień do weryfikacji");
        if (optionId != VerifyPresenter.OK_REQUEST)
            setResult(Activity.RESULT_CANCELED);
        this.finish();
    }

    @Override
    public void displayInfoAbout(TripRoute trip_route) {

    }

    @Override
    public void displayCommunicate(String s) {
        Toast.makeText(this, s , Toast.LENGTH_SHORT).show();
    }
}
