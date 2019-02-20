package pl.edu.pwr.gotopttk.View.Views;

import android.app.ActionBar;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import okhttp3.Route;
import pl.edu.pwr.gotopttk.Model.Entities.Trip;
import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;
import pl.edu.pwr.gotopttk.Presenter.Presenters.TripRoutesListPresenter;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.ITripRoute;

public class VerifyWithoutPartInActivity extends AppCompatActivity implements ITripRoute{

    private TripRoutesListPresenter tripRoutesPresenter;
    private TripRoutesAdapter tripRoutesAdapter;
    private int guide_id;
    private Trip trip;
    private ProgressBar progressBar;
    private AlertDialog alertDialog;
    public final static int VERIFY_WITHOUT_ID = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_verify_without_part_in);

        Toast.makeText(this, "Wybrano weryfikację bez udziału", Toast.LENGTH_SHORT).show();

        Bundle extras = getIntent().getExtras();

        guide_id = extras.getInt("GUIDE_ID");
        trip = extras.getParcelable("TRIP");
        String activity_name = extras.getString("TRIP_NAME");
        setTitle(activity_name);
        progressBar = findViewById(R.id.progress);
        progressBar.getIndeterminateDrawable().setColorFilter(getResources().getColor(R.color.colorPrimaryDark), android.graphics.PorterDuff.Mode.MULTIPLY);

        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialogTheme);
        builder.setCancelable(false);
        alertDialog = builder.create();
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
    protected void onResume() {
        super.onResume();
        tripRoutesAdapter = new TripRoutesAdapter(this, VERIFY_WITHOUT_ID);
        tripRoutesPresenter = new TripRoutesListPresenter(tripRoutesAdapter);
        tripRoutesPresenter.giveTripRoutesToView(guide_id, trip.id);

        RecyclerView recycler = findViewById(R.id.recyclerForTripRoutes);
        recycler.setLayoutManager(new LinearLayoutManager(this));
        recycler.setHasFixedSize(true);
        recycler.setAdapter(tripRoutesAdapter);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        //super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == VERIFY_WITHOUT_ID && resultCode == RouteInfoActivity.RESULT_DISCARD) {
            setResult(RouteInfoActivity.RESULT_DISCARD);
            finish();
        }
    }

    @Override
    public void displayInfoAbout(TripRoute trip_route) {
        Intent intent = new Intent(this, RouteInfoActivity.class);
        intent.putExtra("TRIP_ROUTE", trip_route);
        intent.putExtra("TRIP_ID", trip.id);
        intent.putExtra("TOURIST", trip.tourist);
        intent.putExtra("GUIDE", guide_id);
        intent.putExtra("DATE", trip.endDate);
        startActivityForResult(intent, VERIFY_WITHOUT_ID);
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
