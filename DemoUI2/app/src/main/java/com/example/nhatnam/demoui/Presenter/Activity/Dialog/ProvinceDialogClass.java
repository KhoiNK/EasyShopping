package com.example.nhatnam.demoui.Presenter.Activity.Dialog;

import android.app.Activity;
import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.ListView;

import com.example.nhatnam.demoui.Presenter.Activity.Adapter.ProvinceAdapter;
import com.example.nhatnam.demoui.Model.Province;
import com.example.nhatnam.demoui.R;

import java.util.List;

/**
 * Created by NHAT NAM on 7/5/2017.
 */

public class ProvinceDialogClass extends Dialog {
    private List<Province> mProvince;
    public Activity c;
    public Context context;
    public int ButtonID;
    private ProvinceDialogClass dialog;

    public ProvinceDialogClass getDialog() {
        return dialog;
    }

    public void setDialog(ProvinceDialogClass dialog) {
        this.dialog = dialog;
    }

    public ProvinceDialogClass(Activity a, List<Province> provinces, int id) {
        super(a);
        // TODO Auto-generated constructor stub
        this.c = a;
        context= a;
        mProvince = provinces;
        ButtonID =id;
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
        lvProvince.setAdapter(new ProvinceAdapter(getContext(), mProvince,ButtonID,dialog));

    }
    public static void Dismiss(){
    }

}
