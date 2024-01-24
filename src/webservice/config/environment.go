package config

// A type representing service's environment.
type Environment string

const (
	DEV  Environment = "[dev]"
	PROD Environment = "[prod]"
)

// Method for checking if environment is production or not.
func (env Environment) IsProduction() bool {
	return env == PROD
}

// Function for validating if obtained environment specification is correct.
func (env Environment) IsValid() bool {
	return env == PROD || env == DEV
}
