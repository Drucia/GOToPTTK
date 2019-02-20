package pl.edu.pwr.gotopttk.View.Views;


import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;

public class PlanningPagerAdapter extends FragmentPagerAdapter {

    public PlanningPagerAdapter(FragmentManager fm) {
        super(fm);
    }


    /**
     * Return the Fragment associated with a specified position.
     *
     *
     */
    @Override
    public Fragment getItem(int position) {
        if (position == 0)
        {
            return new MapFragment();
        }
        else{
            return new RouteListFragment();
        }
    }

    /**
     * Return the number of views available.
     */
    @Override
    public int getCount() {
        return 2;
    }
}
