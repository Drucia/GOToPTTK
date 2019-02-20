package pl.edu.pwr.gotopttk.View.Views;

import android.content.Intent;
import android.os.Parcelable;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.widget.Button;

import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;
import pl.edu.pwr.gotopttk.Presenter.Presenters.CreateTripPresenter;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.CreateTripView;

public class CreateTripActivity extends AppCompatActivity implements CreateTripView{

    private PlannedTrip plannedTrip;
    private CreateTripPresenter presenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_trip);

        presenter = new CreateTripPresenter(this);
        Bundle extras = getIntent().getExtras();
        if(extras != null){
            plannedTrip = extras.getParcelable(getString(R.string.EXTRA_PLANNED_TRIP));
        }
        else{
            plannedTrip = new PlannedTrip();
        }

        RecyclerView recyclerView = findViewById(R.id.tripRoutesRecyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.setHasFixedSize(true);

        TripRouteAdapter adapter = new TripRouteAdapter(this);
        adapter.setRoutes(plannedTrip.getRoutes());
        recyclerView.setAdapter(adapter);

        FloatingActionButton addButton = findViewById(R.id.floatingAddButton);
        addButton.setOnClickListener(view -> openPlanningActivity());

        Button endButton = findViewById(R.id.endButton);
        endButton.setOnClickListener(view -> {
            if(!plannedTrip.isEmpty()){
               presenter.createTrip(plannedTrip);
            }
            });
        }

    private void openPlanningActivity(){
        Intent intent = new Intent(this, PlanningActivity.class);
        intent.putExtra(getString(R.string.EXTRA_PLANNED_TRIP), plannedTrip);
        startActivity(intent);
    }

    @Override
    public void openFinishedTripView(PlannedTrip trip) {
        Intent intent = new Intent(this, TripCreatedActivity.class);
        intent.putExtra(getString(R.string.EXTRA_PLANNED_TRIP), plannedTrip);
        startActivity(intent);
    }


    /**
     * Take care of popping the fragment back stack or finishing the activity
     * as appropriate.
     */
    @Override
    public void onBackPressed() {
        resetPlannedTrip();
    }


    private void resetPlannedTrip(){
        Intent intent = new Intent(this, PlanningActivity.class);
        startActivity(intent);
    }


    @Override
    public void displayErrorMessage() {

    }
}
