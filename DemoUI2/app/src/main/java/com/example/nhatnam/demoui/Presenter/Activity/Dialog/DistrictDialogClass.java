package com.example.nhatnam.demoui.Presenter.Activity.Dialog;

import android.app.Activity;
import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.ListView;

import com.example.nhatnam.demoui.Presenter.Activity.Adapter.DistrictAdapter;
import com.example.nhatnam.demoui.Model.District;
import com.example.nhatnam.demoui.R;

import java.util.List;

/**
 * Created by NHAT NAM on 7/5/2017.
 */

public class DistrictDialogClass extends Dialog {
    private List<District> mDistrict;
    public Activity c;
    public Context context;
    public int ButtonID;
    private DistrictDialogClass dialog;

    public DistrictDialogClass getDialog() {
        return dialog;
    }

    public void setDialog(DistrictDialogClass dialog) {
        this.dialog = dialog;
    }

    public DistrictDialogClass(Activity a, List<District> district, int id) {
        super(a);
        // TODO Auto-generated constructor stub
        this.c = a;
        context= a;
        mDistrict = district;
        ButtonID=id;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.address_dialog);
        ViewGroup.LayoutParams params = getWindow().getAttributes();
        params.width = ViewGroup.LayoutParams.WRAP_CONTENT;
        getWindow().setAttributes((android.view.WindowManager.LayoutParams) params);

        ListView lvProvince= (ListView) findViewById(R.id.list_address);
        lvProvince.setAdapter(new DistrictAdapter(getContext(), mDistrict,ButtonID,dialog));

    }


}