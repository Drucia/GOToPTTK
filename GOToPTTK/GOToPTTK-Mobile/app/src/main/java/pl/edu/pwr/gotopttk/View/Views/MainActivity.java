package pl.edu.pwr.gotopttk.View.Views;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import pl.edu.pwr.gotopttk.R;

public class MainActivity extends AppCompatActivity {

    private Button planButton;
    private Button verifyButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        planButton = findViewById(R.id.main_plan_button);
        planButton.setOnClickListener(view -> {
            Intent planningIntent = new Intent(MainActivity.this, PlanningActivity.class);
            MainActivity.this.startActivity(planningIntent);
        });

        verifyButton = findViewById(R.id.main_verify_button);
        verifyButton.setOnClickListener(view -> {
            Intent verifyIntent = new Intent(this, TripListActivity.class);
            startActivity(verifyIntent);
        });
    }
}
