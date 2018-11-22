#include <SPI.h>
#include <MFRC522.h>

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
}

/*
 * Criação do looping de execução.
 */
void loop() {
	read_rfid_card();
}

void configure_rfid() {
  Serial.begin(9600);
	
	while (!Serial) {
	  SPI.begin();
	}
	
	mfrc522.PCD_Init();
	mfrc522.PCD_DumpVersionToSerial();
	
	Serial.println(F("Scan PICC to see UID, SAK, type, and data blocks..."));
}

void configure_valve() {
  pinMode(7, OUTPUT);
}

void read_rfid_card() {
	if (!mfrc522.PICC_IsNewCardPresent()) {
		return;
	}

	if (!mfrc522.PICC_ReadCardSerial()) {
		return;
	}

	mfrc522.PICC_DumpToSerial(&(mfrc522.uid));
	
	open_valve();
}

void open_valve() {
  digitalWrite(7, HIGH);
}

void close_valve() {
  digitalWrite(7, LOW);
}