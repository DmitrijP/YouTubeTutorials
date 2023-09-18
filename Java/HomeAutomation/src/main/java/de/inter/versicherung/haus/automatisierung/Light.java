package de.inter.versicherung.haus.automatisierung;

public class Light {
    private static final int MAX_POWER = 100;
    private static final int MIN_POWER = 0;
    private final String lightName;
    private boolean isOn;
    private int power;

    public Light() {
        isOn = false;
        power = 0;
        lightName = "not named";
    }

    public Light(String lightName) {
        this.lightName = lightName;
        isOn = false;
        power = 0;
    }

    public String getLightName(){
        return lightName;
    }

    public void toggleOnState(){
        isOn = !isOn;
    }

    public boolean getState(){
        return isOn;
    }

    public void increasePowerByTen(){
        if(power < MAX_POWER){
            power = power + 10;
        }
    }

    public void decreasePowerByTen(){
        if(power > MIN_POWER){
            power = power - 10;
        }
    }

    public int getPower(){
        return power;
    }

    @Override
    public String toString() {
        return "Light{" +
                "isOn=" + isOn +
                ", power=" + power +
                '}';
    }
}
