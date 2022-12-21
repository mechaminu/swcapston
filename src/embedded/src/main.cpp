#include <mbed.h>
#include <SimpleFOC.h>

#define GEARBOX_RATIO (47.666)
#define RAD2DEG       (180 / PI)

BLDCMotor motor = BLDCMotor(11);
BLDCDriver6PWM driver = BLDCDriver6PWM(8, 11, 9, 12, 10, 13);
MagneticSensorSPI sensor = MagneticSensorSPI(AS5147_SPI, 5);

MbedSPI spi(4,3,2);

unsigned long last;
float offset;

void setup() {
  Serial.begin(115200);

  sensor.init(&spi);
  motor.linkSensor(&sensor);

  driver.voltage_power_supply = 12;
  driver.voltage_limit = 3;
  driver.dead_zone = 0;
  driver.init();

  motor.linkDriver(&driver);

  motor.foc_modulation = FOCModulationType::SpaceVectorPWM;
  motor.controller = MotionControlType::torque;

  motor.voltage_limit = 12;
  motor.voltage_sensor_align = 1;

  motor.init();
  motor.initFOC();

  motor.target = 0;
  motor.disable();

  offset = sensor.getAngle();
}

void loop() {

  if (motor.enabled && millis() - last > 100) motor.disable();

  motor.loopFOC();
  motor.move();

  if (Serial.available()) {
    
    last = millis(); 
    
    float val;
    if (Serial.read() == 0x02) {
      byte buf[4];
      Serial.readBytes(buf, 4);
      memcpy(&val, buf, 4);
    }

    if (Serial.read() == 0x04) {
      if (!motor.enabled) motor.enable();
      motor.target = val;
    }
    
    while (Serial.available()) Serial.read();
  }
  
  float ang = (sensor.getAngle() - offset) / GEARBOX_RATIO * RAD2DEG;
  Serial.write(0x02);
  Serial.write((byte*)&ang,4);
  Serial.write(0x04);
}