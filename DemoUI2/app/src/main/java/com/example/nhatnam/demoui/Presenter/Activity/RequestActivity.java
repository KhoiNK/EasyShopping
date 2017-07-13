package com.example.nhatnam.demoui.Presenter.Activity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.util.Log;
import android.widget.ListView;

import com.example.nhatnam.demoui.Model.API.ShippingAPI;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;
import com.example.nhatnam.demoui.Model.ShippingDetail;
import com.example.nhatnam.demoui.Presenter.Activity.Adapter.RequestAdapter;
import com.example.nhatnam.demoui.R;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by NHAT NAM on 7/12/2017.
 */

public class RequestActivity extends Activity {
    private static final int MAIN_ACTIVITY = 1;
    public static boolean isModified = false;
    private ShippingAPI shippingAPI;
    private ListView lvRequest;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.request_activity);
        show();
    }

    public void show() {
        loadData(new Listener() {
            @Override
            public void onResult(List<ShippingDetail> shippingDetails) {

                lvRequest = (ListView) findViewById(R.id.list_request);
                lvRequest.setAdapter(new RequestAdapter(RequestActivity.this, shippingDetails));
            }
        });
    }

    private interface Listener {
        void onResult(List<ShippingDetail> shippingDetails);
    }

    public void loadData(final Listener listener) {
        try {
            shippingAPI = RetrofitUtils.get().create(ShippingAPI.class);
            shippingAPI.getRequestOrder(MainActivity.user.getID()).enqueue(new Callback<List<ShippingDetail>>() {
                @Override
                public void onResponse(Call<List<ShippingDetail>> call, Response<List<ShippingDetail>> response) {
                    listener.onResult(response.body());
                }

                @Override
                public void onFailure(Call<List<ShippingDetail>> call, Throwable t) {
                    Log.d("Loi request detail:", t.getMessage());
                }
            });
        } catch (Exception e) {
            Log.d("loi loadData", e.getMessage());
        }
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        if (isModified) {
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }
    }
}