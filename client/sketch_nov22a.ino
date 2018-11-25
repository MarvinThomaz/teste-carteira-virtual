#include <SPI.h>
#include <MFRC522.h>
#include <Bridge.h>
#include <HttpClient.h>
/*
 * Variáveis globais com as portas do dispositivo RFID
 */
#define RST_PIN 9
#define SS_PIN 10

/*
 * Variável de controle do dispositivo RFID
 */
MFRC522 mfrc522(SS_PIN, RST_PIN);

/*
 * Inicialização das portas.
 */
void setup() {
  configure_rfid();
  configure_valve();
  configure_http();
}

/*
 * Criação do looping de execução.
 */
void loop() {
  read_rfid_card();
}

/*
 * Configuração do rfid
 */
void configure_rfid() {
  Serial.begin(9600);
  
  while (!Serial) {
    SPI.begin();
  }
  
  mfrc522.PCD_Init();
  mfrc522.PCD_DumpVersionToSerial();
  
  Serial.println(F("Scan PICC to see UID, SAK, type, and data blocks..."));
}

/*
 * Configuração da valvula
 */
void configure_valve() {
  pinMode(7, OUTPUT);
}

/*
 * Leitura da pulseira
 */
void read_rfid_card() {
  if (!mfrc522.PICC_IsNewCardPresent()) {
    return;
  }

  if (!mfrc522.PICC_ReadCardSerial()) {
    return;
  }

  unsigned long int uid;
  
  uid = convert_uid(mfrc522);

  char* url = { "https://chopp-cart.herokuapp.com/api/carts/" + uid };

  String data = request(url);
  
  open_valve();
}

/*
 * Converte o id da pulseira para long
 */
char* convert_uid(MFRC522 mfrc522)
{
  char* UID_unsigned;
  
  UID_unsigned =  mfrc522.uid.uidByte[0];
  UID_unsigned += mfrc522.uid.uidByte[1];
  UID_unsigned += mfrc522.uid.uidByte[2];
  UID_unsigned += mfrc522.uid.uidByte[3];
  
  return UID_unsigned;
}

/*
 * Abre a válvula
 */
void open_valve() {
  digitalWrite(7, HIGH);
}

/*
 * Fecha a valvula
 */
void close_valve() {
  digitalWrite(7, LOW);
}

/*
 * Configura cliente http
 */
void configure_http() {
    pinMode(13, OUTPUT);
    
    digitalWrite(13, LOW);
    
    Bridge.begin();
    
    Serial.begin(9600);
    
    while(!Serial);
}

/*
 * Realiza requisição de busca do cliente
 */
String request(const char* url) {
  HttpClient http;
  
  int index = 0, result = http.get(url);
  
  char data[300];
  
  while (http.available()) {
    data[index] = http.read();
    
    Serial.print(data);
    
    index++;
  }
  
  Serial.flush();

  delay(5000);

  return data;
}