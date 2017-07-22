package com.example.nhatnam.demoui.Presenter.Activity;

import android.app.Activity;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.util.Log;
import android.widget.ListView;

import com.example.nhatnam.demoui.Model.API.ShippingAPI;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;
import com.example.nhatnam.demoui.Model.ShippingDetail;
import com.example.nhatnam.demoui.Presenter.Activity.Adapter.ApprovedAdapter;
import com.example.nhatnam.demoui.R;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by NHAT NAM on 7/11/2017.
 */

public class ApprovedActivity extends Activity {
    private ShippingAPI shippingAPI;
    private ListView lvRequest;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.request_activity);
        show();
    }

    public void show(){
        loadData(new Listener() {
            @Override
            public void onResult(List<ShippingDetail> shippingDetails) {

                lvRequest = (ListView) findViewById(R.id.list_request);
                lvRequest.setAdapter(new ApprovedAdapter(ApprovedActivity.this,shippingDetails));
            }
        });
    }
    private interface Listener{
        void onResult(List<ShippingDetail> shippingDetails);
    }
    public void loadData(final Listener listener) {

        shippingAPI = RetrofitUtils.get().create(ShippingAPI.class);
        shippingAPI.getApprovedOrder(MainActivity.user.getID()).enqueue(new Callback<List<ShippingDetail>>() {
            @Override
            public void onResponse(Call<List<ShippingDetail>> call, Response<List<ShippingDetail>> response) {
                listener.onResult(response.body());
            }

            @Override
            public void onFailure(Call<List<ShippingDetail>> call, Throwable t) {
                Log.d("Loi request detail:", t.getMessage());
            }
        });
    }

}
