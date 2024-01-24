package config

import (
	"analytics-service/config/errors"
	"os"

	"github.com/spf13/viper"
)

// A structure encapsulating app's configuration.
type Config struct {
	Port     int
	DbString string
	Env      Environment
}

// This function reads app's configuration from a specified config file.
func ReadCfgFromFile(path string) (Config, error) {
	viper.SetConfigFile(path)
	if err := viper.ReadInConfig(); err != nil {
		return Config{}, err
	}

	dbConnString := os.Getenv("PROD_DB_STRING")
	if dbConnString == "" {
		dbConnString = viper.GetString("db_string")
	}

	env := Environment(viper.GetString("env"))
	if !env.IsValid() {
		return Config{}, errors.ErrInvalidEnvironment
	}

	return Config{
		Port:     viper.GetInt("port"),
		DbString: dbConnString,
		Env:      env,
	}, nil
}
