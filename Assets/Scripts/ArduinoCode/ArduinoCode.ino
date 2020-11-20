#define joyX A1
#define joyY A0
#define joyX2 A3
#define joyY2 A2

int xValue, yValue, x2Value, y2Value;
//lightsout
const int green = 7;
const int red   = 5;
const int blue  = 3;
//botón
const int throwball = 53;
const int pickball = 51;
const int ledPin = 13;
int buttonState = 0;
int buttonState2 = 0;

void setup() {
  Serial.begin(9600);
  Serial.setTimeout(10);
  //lightsout
  pinMode(green, OUTPUT);
  pinMode(red, OUTPUT);
  pinMode(blue, OUTPUT);

  //botón
  pinMode(ledPin, OUTPUT);
  pinMode(throwball, INPUT);
  pinMode(pickball, INPUT);

}

void loop() {
  xValue = analogRead(joyX);
  yValue = analogRead(joyY);
  x2Value = analogRead(joyX2);
  y2Value = analogRead(joyY2);

  buttonState = digitalRead(throwball);
  buttonState2 = digitalRead(pickball);
  if(buttonState == HIGH){
    digitalWrite(ledPin, HIGH);
  }
  else{
    digitalWrite(ledPin, LOW);
  }
  
  if(buttonState2 == HIGH){
    digitalWrite(ledPin, HIGH);
  }
  else{
    digitalWrite(ledPin, LOW);
  }
  
  //datos enviados a Unity
  
  if (Serial.readString() == "ON"){
    lightsOn();
  }

  Serial.print(xValue);
  Serial.print(" ");
  Serial.print(yValue);
  Serial.print(" ");
  Serial.print(x2Value);
  Serial.print(" ");
  Serial.print(y2Value);
  Serial.print(" ");
  Serial.print(buttonState);
  Serial.print(" ");
  Serial.print(buttonState2);
  Serial.print('\n');

  delay(10);
}

void lightsOn() {
  digitalWrite(green, HIGH);
  delay(200);
  digitalWrite(green, LOW);
  delay(100);
  digitalWrite(red, HIGH);
  delay(200);
  digitalWrite(red, LOW);
  delay(100);
  digitalWrite(blue, HIGH);
  delay(200); 
  digitalWrite(blue, LOW); 
}
