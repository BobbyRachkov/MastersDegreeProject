const int pitchPin=A1;
const int rollPin=A2;

long index=0;
void setup() {
  Serial.begin(9600);

  while (!Serial.available()) {  }   //Wait for user input
  ReadAndResetSerialBoudRate();

  pinMode(pitchPin,INPUT);
  pinMode(rollPin,INPUT);

}

void loop() {

  if (Serial.available ())
  {
    ReadAndResetSerialBoudRate();
  }

  int pitch=analogRead(pitchPin);
  int roll=analogRead(rollPin);
  String empty="";
  String d=";";
  Serial.println(d+ pitch +d+ roll +d+ millis() +d+ index++);
}

void ReadAndResetSerialBoudRate()
{
  long newBoudRate = Serial.parseInt();
  //Serial.println(newBoudRate);
  Serial.end();
  Serial.begin(newBoudRate);
}