package errors

import "errors"

var (
	ErrInvalidEnvironment = errors.New("specified environment is not valid")
)
