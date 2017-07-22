package com.example.nhatnam.demoui.Presenter.Activity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.View;
import android.widget.Button;

import com.example.nhatnam.demoui.Model.API.AddressAPI;
import com.example.nhatnam.demoui.Presenter.Activity.Dialog.DistrictDialogClass;
import com.example.nhatnam.demoui.Presenter.Activity.Dialog.ProvinceDialogClass;
import com.example.nhatnam.demoui.Model.District;
import com.example.nhatnam.demoui.Model.Province;
import com.example.nhatnam.demoui.R;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by NHAT NAM on 7/4/2017.
 */

public class SearchActivity extends Activity implements View.OnClickListener {
    private AddressAPI mAddressAPI;
    private List<Province> listProvince;
    private List<District> listDistrict;
    public static Button from_province;
    public static Button from_district;
    public static Button to_province;
    public static Button to_district;
    public static Province fprovince;
    public static District fdistrict;
    public static Province tprovince;
    public static District tdistrict;
    private Button btnSearch;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.search_orders);
        from_province = (Button) findViewById(R.id.from_city);
        from_province.setOnClickListener(this);
        from_district = (Button) findViewById(R.id.from_district);
        from_district.setOnClickListener(this);
        to_province = (Button) findViewById(R.id.to_city);
        to_province.setOnClickListener(this);
        to_district = (Button) findViewById(R.id.to_district);
        to_district.setOnClickListener(this);
        btnSearch = (Button) findViewById(R.id.btnSearch);
        btnSearch.setOnClickListener(this);
        getProvince();
    }

    void getProvince() {
        mAddressAPI = RetrofitUtils.get().create(AddressAPI.class);
        mAddressAPI.getProvinces().enqueue(new Callback<List<Province>>() {
            @Override
            public void onResponse(Call<List<Province>> call, Response<List<Province>> response) {
                listProvince = response.body();


            }

            @Override
            public void onFailure(Call<List<Province>> call, Throwable t) {
                Log.d("Loi province", t.getMessage());

            }
        });
    }

    private interface DistrictListener {
        void onResult(List<District> district);
    }

    private void getDistrictFrom(int ProvinceID) {
        fetchDistrict(new DistrictListener() {
            @Override
            public void onResult(List<District> district) {
                listDistrict = district;
                try {
                    DistrictDialogClass cdd = new DistrictDialogClass(SearchActivity.this, listDistrict, R.id.from_district);
                    cdd.setDialog(cdd);
                    cdd.show();
                } catch (Exception e) {
                    Log.d("loi from district", e.getMessage());
                }
            }
        }, ProvinceID);
    }

    private void getDistrictTo(int ProvinceID) {
        fetchDistrict(new DistrictListener() {
            @Override
            public void onResult(List<District> district) {
                listDistrict = district;
                try {
                    DistrictDialogClass cdd = new DistrictDialogClass(SearchActivity.this, listDistrict, R.id.to_district);
                    cdd.setDialog(cdd);
                    cdd.show();
                } catch (Exception e) {
                    Log.d("loi to district", e.getMessage());
                }
            }
        }, ProvinceID);
    }

    void fetchDistrict(final DistrictListener listener, int ProvinceID) {
        mAddressAPI = RetrofitUtils.get().create(AddressAPI.class);
        mAddressAPI.getDistricts(ProvinceID).enqueue(new Callback<List<District>>() {
            @Override
            public void onResponse(Call<List<District>> call, Response<List<District>> response) {
//                listDistrict = response.body();
                listener.onResult(response.body());
                Log.d("size District", String.valueOf(listDistrict.size()));

            }

            @Override
            public void onFailure(Call<List<District>> call, Throwable t) {
                Log.d("Loi district", t.getMessage());

            }
        });
    }

    @Override
    public void onClick(View view) {
        int id = view.getId();
        if (id == R.id.from_city) {
            ProvinceDialogClass cdd = new ProvinceDialogClass(SearchActivity.this, listProvince, R.id.from_city);
            cdd.setDialog(cdd);
            cdd.show();
        } else if (id == R.id.from_district) {
            try {
                if (fprovince.getName().length() > 1) {
                    getDistrictFrom(fprovince.getId());
                }
            } catch (Exception e) {
                Log.d("Loi fprovince.getName()", e.getMessage());

            }
        } else if (id == R.id.to_city) {
            ProvinceDialogClass cdd = new ProvinceDialogClass(SearchActivity.this, listProvince, R.id.to_city);
            cdd.setDialog(cdd);
            cdd.show();
        } else if (id == R.id.to_district) {
            try {
                if (tprovince.getName().length() > 1) {
                    getDistrictTo(tprovince.getId());

                }
            } catch (Exception e) {
                Log.d("Loi tprovince.getName()", e.getMessage());
            }
        } else if (id == R.id.btnSearch) {
            int fromCity;
            int fromDistrict;
            int toCity;
            int toDistrict;
            if (fprovince != null) {
                fromCity = fprovince.getId();
            } else fromCity = 0;
            if (fdistrict != null)
                fromDistrict = fdistrict.getId();
            else fromDistrict = 0;

            if (tprovince != null)
                toCity = tprovince.getId();
            else toCity = 0;

            if (tdistrict != null)
                toDistrict = tdistrict.getId();
            else toDistrict = 0;
            Intent intent = new Intent(SearchActivity.this, MainActivity.class);
            intent.putExtra("fromCity", fromCity);
            intent.putExtra("fromDistrict", fromDistrict);
            intent.putExtra("toCity", toCity);
            intent.putExtra("toDistrict", toDistrict);
            MainActivity.STATUS = 1;
            fprovince = null;
            fdistrict = null;
            tprovince = null;
            tdistrict = null;
            startActivity(intent);
            SearchActivity.this.finish();


        }
    }
}
