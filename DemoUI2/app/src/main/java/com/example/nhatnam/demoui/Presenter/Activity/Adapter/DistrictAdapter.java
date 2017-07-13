package com.example.nhatnam.demoui.Presenter.Activity.Adapter;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.RadioButton;
import android.widget.TextView;

import com.example.nhatnam.demoui.Presenter.Activity.Dialog.DistrictDialogClass;
import com.example.nhatnam.demoui.Presenter.Activity.SearchActivity;
import com.example.nhatnam.demoui.Model.District;
import com.example.nhatnam.demoui.R;

import java.util.List;

/**
 * Created by NHAT NAM on 7/5/2017.
 */

public class DistrictAdapter extends ArrayAdapter<District> {
    private List<District> mDistrict;
    private DistrictDialogClass dialog;
    private int buttonID;
    public DistrictAdapter(@NonNull Context context, List<District> objects, int id,DistrictDialogClass dialog) {
        super(context, -1);
        mDistrict = objects;
        buttonID=id;
        this.dialog=dialog;
    }
    @Nullable
    @Override
    public District getItem(int position) {
        return mDistrict.get(position);
    }

    @Override
    public int getCount() {
        return mDistrict.size();
    }
    @NonNull
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        final ViewHolder viewHolder;
        final int post= position;
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext())
                    .inflate(R.layout.address_item, parent, false);
            viewHolder = new ViewHolder();
            viewHolder.tvAdress = (TextView) convertView.findViewById(R.id.tvAddress);
            viewHolder.radioButton = (RadioButton) convertView.findViewById(R.id.rdAddress);
            viewHolder.radioButton.setEnabled(false);
            convertView.setTag(viewHolder);
            convertView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    viewHolder.radioButton.setChecked(true);
                    if(buttonID >-1){
                        if (buttonID == R.id.from_district) {
                            SearchActivity.fdistrict = findByName(viewHolder.tvAdress.getText().toString());
                            SearchActivity.from_district.setText(SearchActivity.fdistrict.getName());
                        } else {
                            SearchActivity.tdistrict = findByName(viewHolder.tvAdress.getText().toString());
                            SearchActivity.to_district.setText(SearchActivity.tdistrict.getName());

                        }
                    }
                    dialog.dismiss();
                }
            });
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }
        District province = getItem(position);
        // Fill data
        try {
            viewHolder.tvAdress.setText(province.getName());

        }catch (Exception e){
            Log.d("Loi province adapter",position+", "+e.getMessage());
        }
        return convertView;
    }
    private District findByName(String name){
        for(int i=0;i<mDistrict.size();i++){
            if(mDistrict.get(i).getName().equals(name))
                return mDistrict.get(i);
        }
        return null;
    }
    private class ViewHolder{
        TextView tvAdress;
        RadioButton radioButton;
    }

}