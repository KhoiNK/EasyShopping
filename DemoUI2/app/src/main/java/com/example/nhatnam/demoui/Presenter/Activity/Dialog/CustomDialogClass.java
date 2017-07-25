package com.example.nhatnam.demoui.Presenter.Activity.Dialog;

import android.app.Activity;
import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import com.example.nhatnam.demoui.Model.API.OrderAPI;
import com.example.nhatnam.demoui.Presenter.Activity.MainActivity;
import com.example.nhatnam.demoui.Presenter.Activity.Adapter.OrdersAdapter;
import com.example.nhatnam.demoui.Model.Order;
import com.example.nhatnam.demoui.R;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by NHAT NAM on 6/3/2017.
 */

public class CustomDialogClass extends Dialog implements View.OnClickListener {
    private List<Order> mOrders;
    public Activity c;
    public Context context;
    private Button btnTakeOrder;
    private OrderAPI mOrderAPI;

    public CustomDialogClass getCdd() {
        return cdd;
    }

    public void setCdd(CustomDialogClass cdd) {
        this.cdd = cdd;
    }

    private CustomDialogClass cdd;

    public CustomDialogClass(Activity a, List<Order> orders) {
        super(a);
        // TODO Auto-generated constructor stub
        this.c = a;
        context = a;
        mOrders = orders;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.custom_dialog);
        ViewGroup.LayoutParams params = getWindow().getAttributes();
        params.width = ViewGroup.LayoutParams.MATCH_PARENT;
        getWindow().setAttributes((android.view.WindowManager.LayoutParams) params);
        btnTakeOrder = (Button) findViewById(R.id.order_check);
        btnTakeOrder.setOnClickListener(this);
        ListView lvOrders = (ListView) findViewById(R.id.list_orders);
        lvOrders.setAdapter(new OrdersAdapter(getContext(), mOrders, cdd));
    }


    @Override
    public void onClick(View view) {
        int count = 0;
        int i = 0;
        while (i < mOrders.size()) {
            if (mOrders.get(i).isChecked) {
                takeOrder(MainActivity.user.getID(), mOrders.get(i).getID());
                count++;
                mOrders.remove(i);
            } else
                i++;
        }
        cdd.dismiss();
        Toast toast = Toast.makeText(context, "Sent success " + count + " request", Toast.LENGTH_SHORT);
        toast.show();
    }

    void takeOrder(int UserID, int OrderID) {
        mOrderAPI = RetrofitUtils.get().create(OrderAPI.class);
        try {
            mOrderAPI.takeOrder(OrderID, UserID).enqueue(new Callback<Integer>() {
                @Override
                public void onResponse(Call<Integer> call, Response<Integer> response) {
//                    Log.d("dung", response.body().toString());
                }

                @Override
                public void onFailure(Call<Integer> call, Throwable t) {
                    Log.d("loi lay order", t.getMessage());
                }
            });
        }catch (Exception e){
            Log.d("loi lay order catch", e.getMessage());
        }
    }
}