package pl.edu.pwr.gotopttk.View.Views;


import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import pl.edu.pwr.gotopttk.R;

public class ErrorDialogFragment extends DialogFragment {
    private Context context;

    private final static String EXTRAS_KEY_MESSAGE = "EXTRAS_KEY_MESSAGE";
    private final static String EXTRAS_KEY_TITLE = "EXTRAS_KEY_TITLE";
    private final static String EXTRAS_KEY_BUTTON = "EXTRAS_KEY_BUTTON";

    private String message;
    private String title;
    private String buttonText;

    public ErrorDialogFragment() {
        context = getActivity();
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        Bundle extras = getArguments();
        if (extras != null) {
            message = extras.getString(EXTRAS_KEY_MESSAGE, getString(R.string.DIALOG_DEFAULT_MESSAGE));
            title = extras.getString(EXTRAS_KEY_TITLE, getString(R.string.DIALOG_DEFAULT_TITLE));
            buttonText = extras.getString(EXTRAS_KEY_BUTTON, getString(R.string.DIALOG_DEFAULT_BUTTON_TEXT));
        }

        super.onCreate(savedInstanceState);
    }



    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        return super.onCreateView(inflater, container, savedInstanceState);
    }

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity(), R.style.AlertDialogTheme);


        builder.setMessage(message)
                .setTitle(title);

        builder.setPositiveButton(buttonText, (dialog, id) -> {
            dialog.dismiss();
        });

        return builder.create();
    }

    public static ErrorDialogFragment newInstance(String message, String title, String buttonText) {
        ErrorDialogFragment f = new ErrorDialogFragment();
        Bundle extras = new Bundle();

        extras.putString(EXTRAS_KEY_MESSAGE, message);
        extras.putString(EXTRAS_KEY_TITLE, title);
        extras.putString(EXTRAS_KEY_BUTTON, buttonText);

        f.setArguments(extras);

        return f;
    }
}

