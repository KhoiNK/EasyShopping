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

import com.example.nhatnam.demoui.Presenter.Activity.Dialog.ProvinceDialogClass;
import com.example.nhatnam.demoui.Presenter.Activity.SearchActivity;
import com.example.nhatnam.demoui.Model.Province;
import com.example.nhatnam.demoui.R;

import java.util.List;

/**
 * Created by NHAT NAM on 7/5/2017.
 */

public class ProvinceAdapter extends ArrayAdapter<Province> {
    private List<Province> mProvince;
    private int buttonID;
    ProvinceDialogClass dialog;
    public ProvinceAdapter(@NonNull Context context, List<Province> objects, int id, ProvinceDialogClass dialog) {
        super(context, -1);
        mProvince= objects;
        buttonID=id;
        this.dialog=dialog;
    }
    @Nullable
    @Override
    public Province getItem(int position) {
        return mProvince.get(position);
    }

    @Override
    public int getCount() {
        return mProvince.size();
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
                    if(buttonID==R.id.from_city){
                        SearchActivity.fprovince=findByName(viewHolder.tvAdress.getText().toString());
                        SearchActivity.from_province.setText(SearchActivity.fprovince.getName());

                    }else{
                        SearchActivity.tprovince=findByName(viewHolder.tvAdress.getText().toString());
                        SearchActivity.to_province.setText(SearchActivity.tprovince.getName());
                    }
                    dialog.dismiss();
                }
            });
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }
        Province province = getItem(position);
        // Fill data
        try {
            viewHolder.tvAdress.setText(province.getName());

        }catch (Exception e){
            Log.d("Loi province adapter",position+", "+e.getMessage());
        }
        return convertView;
    }

    private Province findByName(String name){
        for(int i=0;i<mProvince.size();i++){
            if(mProvince.get(i).getName().equals(name))
                return mProvince.get(i);
        }
        return null;
    }
    private class ViewHolder{
        TextView tvAdress;
        RadioButton radioButton;
    }
}
