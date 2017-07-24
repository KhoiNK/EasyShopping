package com.example.nhatnam.demoui.Model;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by NHAT NAM on 7/22/2017.
 */

public class ListPoint {
    private List<Point> list;

    public ListPoint(List<Point> list) {
        this.list = list;
    }

    public ListPoint() {
    }

    public List<Point> getList() {
        return list;
    }

    public void setList(List<Point> list) {
        this.list = list;
    }

    public void add(Point point){
        if(list==null){
            list=new ArrayList<>();
        }
        list.add(point);
    }
    public void removeLast(){
        if(list.size()>0){
            list.remove(list.size()-1);
        }
    }
    public void determineDistance(){
        for (Point point:list){
            point.findDistance();
        }
    }
    public List<Point> Clone(){
        List<Point> clone= new ArrayList<>();
        for(Point point:list){
            clone.add(new Point(point));
        }
        return clone;
    }
    public double sum(){
        double sum=0;
        for(Point point:list){
            sum=sum+point.getDistance();
        }
        return sum;
    }
}
