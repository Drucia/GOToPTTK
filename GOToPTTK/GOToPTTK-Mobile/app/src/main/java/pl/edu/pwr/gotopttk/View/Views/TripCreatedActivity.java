package pl.edu.pwr.gotopttk.View.Views;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.widget.TextView;

import java.time.LocalDate;
import java.time.ZoneId;
import java.util.Date;

import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;
import pl.edu.pwr.gotopttk.R;

public class TripCreatedActivity extends AppCompatActivity {

    private PlannedTrip plannedTrip;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_trip_planned);

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

        TextView tripDate = findViewById(R.id.tripDate);
        LocalDate date = plannedTrip.getDate().toInstant().atZone(ZoneId.systemDefault()).toLocalDate();
        tripDate.setText(getString(R.string.date_format, date.getDayOfMonth(), date.getMonthValue(), date.getYear()));

        TextView pointsText = findViewById(R.id.tripPoints);
        pointsText.setText(getString(R.string.pointsFormat, plannedTrip.getPoints()));

    }


    @Override
    public void onBackPressed() {
        Intent intent = new Intent(this, MainActivity.class);
        startActivity(intent);
    }
}
