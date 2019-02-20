package pl.edu.pwr.gotopttk.View.Views;

import android.content.Intent;
import android.support.design.widget.BottomNavigationView;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;
import pl.edu.pwr.gotopttk.Model.Entities.Route;
import pl.edu.pwr.gotopttk.Presenter.Presenters.PlanningPresenter;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.PlanningView;

public class PlanningActivity extends AppCompatActivity implements PlanningView {

    private PlanningPresenter presenter = new PlanningPresenter(this);
    private ViewPager viewPager;
    private PlannedTrip plannedTrip;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_planning);


        viewPager = findViewById(R.id.viewpager);
        viewPager.setAdapter(new PlanningPagerAdapter(getSupportFragmentManager()));
        viewPager.setOnTouchListener((view, motionEvent) -> true);


        Bundle extras = getIntent().getExtras();
        if(extras != null){
            plannedTrip = extras.getParcelable(getString(R.string.EXTRA_PLANNED_TRIP));
            openRouteListView();
        }
        else {
            plannedTrip = new PlannedTrip();
        }

        BottomNavigationView bottomNavigationView = findViewById(R.id.bottom_navigation);
        bottomNavigationView.setOnNavigationItemSelectedListener(
                item -> {
                    switch (item.getItemId()) {
                        case R.id.action_map:
                            openMapView();
                            break;
                        case R.id.action_list:
                            openRouteListView();
                            break;
                    }
                    return true;
                });

    }


    public void openMapView() {
        if(viewPager != null){
            viewPager.setCurrentItem(0);
        }

    }


    public void openRouteListView() {
        if(viewPager != null){
            viewPager.setCurrentItem(1);
        }

    }

    @Override
    public PlannedTrip getPlannedTrip() {
        return plannedTrip;
    }

    @Override
    public void openTripView(Route route) {
        plannedTrip.addRoute(route);
        Intent intent = new Intent(this, CreateTripActivity.class);
        intent.putExtra(getString(R.string.EXTRA_PLANNED_TRIP), plannedTrip);
        startActivity(intent);
    }

    @Override
    public void displayPlanningError() {

    }
}
