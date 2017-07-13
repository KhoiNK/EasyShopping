package com.example.nhatnam.demoui.Presenter.Activity.Dialog;

import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;

import com.example.nhatnam.demoui.R;

/**
 * Created by NHAT NAM on 7/12/2017.
 */

public class ConfirmDialog extends Dialog {
    public static String code="";
    public Context context;
    private Button btnOK;
    private EditText editText;

    public ConfirmDialog(@NonNull Context context) {
        super(context);
    }


//    public ConfirmDialog(ApprovedActivity a) {
//        super(a);
//        // TODO Auto-generated constructor stub
//        this.c = a;
//        context = a;
//    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.confirm_dialog);
        ViewGroup.LayoutParams params = getWindow().getAttributes();
        params.width = ViewGroup.LayoutParams.WRAP_CONTENT;
        getWindow().setAttributes((android.view.WindowManager.LayoutParams) params);
        btnOK = (Button) findViewById(R.id.confirm_ok);
        editText = (EditText) findViewById(R.id.confirm_code);
        btnOK.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                code = editText.getText().toString();
                dismiss();
            }
        });
    }

}
