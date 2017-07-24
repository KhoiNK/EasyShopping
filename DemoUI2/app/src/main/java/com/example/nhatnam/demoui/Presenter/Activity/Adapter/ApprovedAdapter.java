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
import android.widget.TextView;
import android.widget.Toast;

import com.example.nhatnam.demoui.Model.API.ShippingAPI;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;
import com.example.nhatnam.demoui.Model.ShippingDetail;
import com.example.nhatnam.demoui.Presenter.Activity.Dialog.ConfirmDialog;
import com.example.nhatnam.demoui.Presenter.Activity.MainActivity;
import com.example.nhatnam.demoui.R;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by NHAT NAM on 7/11/2017.
 */

public class ApprovedAdapter extends ArrayAdapter<ShippingDetail> {
    private static final int REQUEST_ACTIVITY = 2;
    private List<ShippingDetail> mShippingDetail;
    private Context context;
    private ShippingAPI shippingAPI;

    public ApprovedAdapter(@NonNull Context context, List<ShippingDetail> shippingDetails) {
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
        final View finalConvertView;
        final int post = position;
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext())
                    .inflate(R.layout.request_item, parent, false);
            viewHolder = new ViewHolder();
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
            finalConvertView = convertView;
            convertView.setOnLongClickListener(new View.OnLongClickListener() {
                @Override
                public boolean onLongClick(View view) {

                    ConfirmDialog cfd = new ConfirmDialog(context);
                    cfd.show();
                    cfd.setOnDismissListener(new DialogInterface.OnDismissListener() {
                        @Override
                        public void onDismiss(DialogInterface dialogInterface) {
                            int id = Integer.parseInt(viewHolder.tvOrderID.getText().toString());
                            ShippingDetail shippingDetail = mShippingDetail.get(findViewByOrderID(id));
                            if (ConfirmDialog.code.length() > 0) {
                                if (ConfirmDialog.code.equals(shippingDetail.getOrder().getOrderCode())) {
                                    //complete order
                                    try {
                                        shippingAPI = RetrofitUtils.get().create(ShippingAPI.class);
                                        shippingAPI.completeOrder(shippingDetail.getOrder().getID(), MainActivity.user.getID()).enqueue(new Callback<Integer>() {
                                            @Override
                                            public void onResponse(Call<Integer> call, Response<Integer> response) {
                                                finalConvertView.setBackgroundResource(R.color.colorAccent);
                                                finalConvertView.setEnabled(false);
                                                Toast.makeText(getContext(), "Order is completed", Toast.LENGTH_SHORT);
                                            }

                                            @Override
                                            public void onFailure(Call<Integer> call, Throwable t) {
                                                Log.d("onfail complete order", t.getMessage());
                                            }
                                        });
                                    }catch (Exception e){
                                        Log.d("loi complete order", e.getMessage());
                                    }

                                } else {
                                    showWrongDialog();
//                                    Toast.makeText(getContext(), shippingDetail.getOrder().getOrderCode(), Toast.LENGTH_SHORT).show();
                                }
                            }
                        }
                    });
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
        viewHolder.tvPrice.setText(String.valueOf(shippingDetail.getOrder().getPrice())+" VND");
        viewHolder.tvShopName.setText(shippingDetail.getStoreName());
        viewHolder.tvOrderID.setText(String.valueOf(shippingDetail.getOrder().getID()));
        viewHolder.tvDestination.setText(shippingDetail.getOrder().getAddress());
        if (shippingDetail.getOrderStatus() == 3) {
            convertView.setBackgroundResource(R.color.colorAccent);
            convertView.setEnabled(false);
        } else
            convertView.setBackgroundResource(R.color.green);

        return convertView;
    }

    private void showWrongDialog() {
        AlertDialog.Builder builder1 = new AlertDialog.Builder(context);
        builder1.setMessage("Order code is wrong");
        builder1.setCancelable(true);

        builder1.setPositiveButton(
                "OK",
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });
        AlertDialog alert11 = builder1.create();
        alert11.show();
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
        TextView tvOrderID;
        TextView tvPrice;
        TextView tvDestination;
        TextView tvWeight;
        TextView tvShopName;
    }
}
