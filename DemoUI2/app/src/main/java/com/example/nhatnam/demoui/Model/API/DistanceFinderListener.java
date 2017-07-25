package com.example.nhatnam.demoui.Model.API;

import com.example.nhatnam.demoui.Model.Route;

import java.util.List;

/**
 * Created by NHAT NAM on 7/22/2017.
 */

public interface DistanceFinderListener {
    void onDirectionFinderSuccess(List<Route> route);

}
