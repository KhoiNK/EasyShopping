package com.example.nhatnam.demoui.Presenter.Activity.Adapter;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.TextView;

import com.example.nhatnam.demoui.Model.API.OrderAPI;
import com.example.nhatnam.demoui.Presenter.Activity.Dialog.CustomDialogClass;
import com.example.nhatnam.demoui.Presenter.Activity.MainActivity;
import com.example.nhatnam.demoui.Model.Order;
import com.example.nhatnam.demoui.R;

import java.util.List;

import static com.example.nhatnam.demoui.R.id.tvOrderID;

/**
 * Created by NHAT NAM on 6/3/2017.
 */

public class OrdersAdapter extends ArrayAdapter<Order> {
    private List<Order> mOrders;
    private OrderAPI mOrderAPI;
    private SharedPreferences loginPreferences;
    private SharedPreferences.Editor loginPrefsEditor;
    private CustomDialogClass cdd;
    private Context context;

    public OrdersAdapter(@NonNull Context context, List<Order> objects, CustomDialogClass cd) {
        super(context, -1);
        this.context = context;
        mOrders = objects;
        cdd = cd;
    }

    @Nullable
    @Override
    public Order getItem(int position) {
        return mOrders.get(position);
    }

    @Override
    public int getCount() {
        return mOrders.size();
    }

    @NonNull
    @Override
    public View getView(final int position, View convertView, ViewGroup parent) {
        final ViewHolder viewHolder;
        final int post = position;
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext())
                    .inflate(R.layout.order_detail, parent, false);
            viewHolder = new ViewHolder();
            viewHolder.tvOrderID = (TextView) convertView.findViewById(tvOrderID);
            viewHolder.tvPrice = (TextView) convertView.findViewById(R.id.earn_price);
            viewHolder.tvDestination = (TextView) convertView.findViewById(R.id.destination);
            viewHolder.edOrderKey = (EditText) convertView.findViewById(R.id.order_key);
            viewHolder.tvWeight = (TextView) convertView.findViewById(R.id.order_weigth);
            viewHolder.isCheck = (CheckBox) convertView.findViewById(R.id.cbOrderDetail);
            viewHolder.isCheck.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {

                    int id=Integer.parseInt(viewHolder.tvOrderID.getText().toString());
                    mOrders.get(findViewByOrderID(id)).check();
                }
            });
            viewHolder.tvDestination.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    MainActivity.mDestination = viewHolder.tvDestination.getText().toString();
                    cdd.dismiss();
                }
            });
            convertView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }
        Order order = getItem(position);
        // Fill data
        if (order.getOrderDetail() != null) {
            viewHolder.tvPrice.setText(String.valueOf(order.getOrderDetail().getPrice()));
            viewHolder.tvWeight.setText(String.valueOf(order.getOrderDetail().getWeight() / 1000) + " Kg");
        }
        viewHolder.tvOrderID.setText(String.valueOf(order.getID()));
        viewHolder.tvDestination.setText(order.getAddress());
        viewHolder.isCheck.setChecked(getItem(position).isChecked);
        return convertView;
    }


    private int findViewByOrderID(int id) {
        for (int i = 0; i < mOrders.size(); i++) {
            if (mOrders.get(i).getID() == id) {
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
        EditText edOrderKey;
        CheckBox isCheck;
        Button btnCheck;
    }
}
