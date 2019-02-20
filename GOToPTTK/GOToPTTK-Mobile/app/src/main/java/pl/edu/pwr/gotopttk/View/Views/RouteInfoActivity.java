package pl.edu.pwr.gotopttk.View.Views;

import android.app.ActionBar;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import pl.edu.pwr.gotopttk.Model.Entities.Tourist;
import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;
import pl.edu.pwr.gotopttk.Model.Entities.VerifyRequest;
import pl.edu.pwr.gotopttk.Presenter.Presenters.PhotoPresenter;
import pl.edu.pwr.gotopttk.Presenter.Presenters.VerifyPresenter;
import pl.edu.pwr.gotopttk.R;
import pl.edu.pwr.gotopttk.View.Interfaces.IVerifyView;
import pl.edu.pwr.gotopttk.View.Interfaces.IVerifyWithout;

public class RouteInfoActivity extends AppCompatActivity implements IVerifyView, IVerifyWithout {
    public static final int RESULT_DISCARD = 5;
    public static final int RESULT_APPLY = 6;
    private VerifyPresenter presenter;
    private VerifyRequest request;
    private TripRoute trip_route;
    private Tourist tourist;
    private int guide_id;
    private int trip_id;
    private String date;
    private Date dat;
    private ImageView start;
    private ImageView end;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_route_info);

        Bundle extras = getIntent().getExtras();
        trip_route = extras.getParcelable("TRIP_ROUTE");
        tourist = extras.getParcelable("TOURIST");
        guide_id = extras.getInt("GUIDE");
        trip_id = extras.getInt("TRIP_ID");
        date = extras.getString("DATE");

        SimpleDateFormat input_format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");

        try {
            dat = input_format.parse(date);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        TextView textView = findViewById(R.id.st_txt);
        textView.setTextColor(Color.GREEN);
        textView.setText(trip_route.state.state);
        textView = findViewById(R.id.tou_txt);
        textView.setText(tourist.user.name + " " + tourist.user.surname);
        textView = findViewById(R.id.poin_txt);
        textView.setText(trip_route.route.points + "");
        textView = findViewById(R.id.start_label_txt);
        textView.setText(trip_route.route.start.name);
        textView = findViewById(R.id.end_label_txt);
        textView.setText(trip_route.route.end.name);

        Button but = findViewById(R.id.butt_apply);
        but.setOnClickListener(view -> this.onClickedAccept());
        but = findViewById(R.id.butt_discard);
        but.setOnClickListener(view -> this.onClickedDiscard());
        presenter = new VerifyPresenter(this);

        PhotoPresenter p = new PhotoPresenter(this);
        p.getImagesToView(trip_route.id, "start");
        p.getImagesToView(trip_route.id, "end");
        start = findViewById(R.id.image_start);
        end = findViewById(R.id.image_end);
        setTitle(trip_route.route.start.name + " - " + trip_route.route.end.name);

//        android.support.v7.app.ActionBar ab = getSupportActionBar();
//        LayoutInflater inflater = getLayoutInflater();
//        View view = inflater.inflate(R.layout.custom_action_bar, null);
//        ((TextView) view.findViewById(R.id.ab_title_start)).setText(trip_route.route.start.name);
//        ((TextView) view.findViewById(R.id.ab_title_end)).setText("- " + trip_route.route.end.name);
//        view.findViewById(R.id.ab_arrow).setOnClickListener(view1 -> finish());

//        ab.setDisplayOptions(ActionBar.DISPLAY_SHOW_CUSTOM);

        // Finally, set the newly created TextView as ActionBar custom view
//        ab.setCustomView(view);
        //setTitle(activity_name);
    }

    private void onClickedDiscard() {
        request = new VerifyRequest(dat, "", trip_id, guide_id, trip_route.id, "ODRZUCONA");
        presenter.createVerify(request);
        Intent resultIntent = new Intent();
        setResult(RESULT_DISCARD, resultIntent);
    }

    private void onClickedAccept() {
        request = new VerifyRequest(dat, "", trip_id, guide_id, trip_route.id, "ZATWIERDZONA");
        presenter.createVerify(request);
        Intent resultIntent = new Intent();
        setResult(Activity.RESULT_OK, resultIntent);
    }

    @Override
    public void displayToast(int optionId) {
        Toast.makeText(this, optionId == VerifyPresenter.OK_REQUEST ? "Poprawnie zweryfikowano odcinek" : "Błąd weryfikacji", Toast.LENGTH_SHORT).show();
        this.finish();
    }

    @Override
    public void putImgesToView(Bitmap photos, String edge)
    {
        if (edge.equals("start"))
            start.setImageBitmap(photos);
        else if (edge.equals("end"))
            end.setImageBitmap(photos);
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
