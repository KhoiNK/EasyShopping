package com.example.nhatnam.demoui.Model.API;

import com.example.nhatnam.demoui.Model.ShippingDetail;

import java.util.List;

/**
 * Created by NHAT NAM on 7/11/2017.
 */

public interface ListenerRequest {
    public void onResult(List<ShippingDetail> shippingDetails);
    public void onStart();
}
