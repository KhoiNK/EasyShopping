package com.example.nhatnam.demoui.Presenter.Activity.Adapter;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.nhatnam.demoui.Model.API.ShippingAPI;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;
import com.example.nhatnam.demoui.Model.ShippingDetail;
import com.example.nhatnam.demoui.Presenter.Activity.MainActivity;
import com.example.nhatnam.demoui.Presenter.Activity.RequestActivity;
import com.example.nhatnam.demoui.R;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by NHAT NAM on 7/12/2017.
 */

public class RequestAdapter extends ArrayAdapter<ShippingDetail> {
    private static final int REQUEST_ACTIVITY = 2;
    private List<ShippingDetail> mShippingDetail;
    private ShippingAPI shippingAPI;
    private Context context;

    public RequestAdapter(@NonNull Context context, List<ShippingDetail> shippingDetails) {
        super(context, -1);
        this.context = context;
        mShippingDetail = shippingDetails;
    }

    @Nullable
    @Override
    public ShippingDetail getItem(int position) {
        return mShippingDetail.get(position);
    }

    @Override
    public int getCount() {
        return mShippingDetail.size();
    }

    @NonNull
    @Override
    public View getView(final int position, View convertView, ViewGroup parent) {
        final ViewHolder viewHolder;
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext())
                    .inflate(R.layout.request_item, parent, false);
            viewHolder = new ViewHolder();
            viewHolder.ivDelete= (ImageView) convertView.findViewById(R.id.ivdelete);
            viewHolder.tvShopName = (TextView) convertView.findViewById(R.id.rqshop_name);
            viewHolder.tvOrderID = (TextView) convertView.findViewById(R.id.rqtvOrderID);
            viewHolder.tvPrice = (TextView) convertView.findViewById(R.id.rqearn_price);
            viewHolder.tvDestination = (TextView) convertView.findViewById(R.id.rqdestination);
            viewHolder.tvWeight = (TextView) convertView.findViewById(R.id.rqorder_weigth);
            convertView.setTag(viewHolder);
            convertView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    int id = Integer.parseInt(viewHolder.tvOrderID.getText().toString());
                    ShippingDetail shippingDetail = mShippingDetail.get(findViewByOrderID(id));
//                    if (shippingDetail.getOrderStatus() != 3) {
                    Intent intent = new Intent(getContext(), MainActivity.class);
                    intent.putExtra("Lat", shippingDetail.getShopLat());
                    intent.putExtra("Lng", shippingDetail.getShopLng());
                    intent.putExtra("Address", shippingDetail.getOrder().getAddress());
                    MainActivity.STATUS = REQUEST_ACTIVITY;
                    getContext().startActivity(intent);
//                    }
                }
            });
            convertView.setOnLongClickListener(new View.OnLongClickListener() {
                @Override
                public boolean onLongClick(View view) {
                    int id = findViewByOrderID(Integer.parseInt(viewHolder.tvOrderID.getText().toString()));
                    showConfirmDialog(id);
                    return false;
                }

            });
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }
        ShippingDetail shippingDetail = getItem(position);
        // Fill data
        if (shippingDetail.getOrder().getOrderDetail() != null) {
            viewHolder.tvWeight.setText(String.valueOf(shippingDetail.getOrder().getOrderDetail().getWeight() / 1000) + " Kg");
        }
        viewHolder.tvPrice.setText(String.valueOf(shippingDetail.getOrder().getPrice()));
        viewHolder.tvShopName.setText(shippingDetail.getStoreName());
        viewHolder.tvOrderID.setText(String.valueOf(shippingDetail.getOrder().getID()));
        viewHolder.tvDestination.setText(shippingDetail.getOrder().getAddress());
            convertView.setBackgroundResource(R.color.colorPrimarylight);
        return convertView;
    }

    private void showConfirmDialog(final int listId){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(context);
        builder1.setMessage("Do you want to cancel Request?");
        builder1.setCancelable(true);

        builder1.setNegativeButton(
                "OK",
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, final int id) {
                        cancelOrder(listId,dialog);
                    }
                });

        builder1.setPositiveButton(
                "Cancel",
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });
        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    private void cancelOrder(final int id,DialogInterface dialog){
        shippingAPI= RetrofitUtils.get().create(ShippingAPI.class);
        shippingAPI.cancelOrder(mShippingDetail.get(id).getOrder().getID(),MainActivity.user.getID()).enqueue(new Callback<Integer>() {
            @Override
            public void onResponse(Call<Integer> call, Response<Integer> response) {
                mShippingDetail.remove(id);
                notifyDataSetChanged();
                RequestActivity.isModified=true;
                Toast.makeText(getContext(),"Cancel Successfull",Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onFailure(Call<Integer> call, Throwable t) {
                Log.d("loi cancel Request",t.getMessage());
            }
        });
        dialog.cancel();
    }

    private int findViewByOrderID(int id) {
        for (int i = 0; i < mShippingDetail.size(); i++) {
            if (mShippingDetail.get(i).getOrder().getID() == id) {
                return i;
            }
        }
        return -1;
    }

    private class ViewHolder {
        ImageView ivDelete;
        TextView tvOrderID;
        TextView tvPrice;
        TextView tvDestination;
        TextView tvWeight;
        TextView tvShopName;
    }
}
