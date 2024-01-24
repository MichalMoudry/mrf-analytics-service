package main

import (
	"analytics-service/config"
	"analytics-service/transport"
	"fmt"
	"log"
	"net/http"
)

func main() {
	fmt.Println("Hello from analytics service! ʕ•ᴥ•ʔ")
	cfg, err := config.ReadCfgFromFile("./config.toml")
	if err != nil {
		log.Fatal(err)
	}

	// Start web server
	fmt.Printf("Trying to start a server on %d port.\n", cfg.Port)
	handler := transport.Initialize(cfg.Port)
	err = http.ListenAndServe(fmt.Sprintf(":%d", handler.Port), handler.Mux)
	if err != nil {
		log.Fatal(err)
	}
}
