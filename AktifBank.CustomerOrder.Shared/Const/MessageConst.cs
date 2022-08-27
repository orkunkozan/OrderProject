namespace AktifBank.CustomerOrder.Shared.Const
{
    public static class MessageConst
    {
        public const string AN_UNEXPECTED_ERROR_OCCURRED = "An unexpected error has occurred";
        public const string CANNOT_BE_EMPTY = "{0} cannot be empty";
        public const string THERE_CANNOT_BE_A_RECORD_PRIECE_LESSTHENEQUAL_ZERO = "There cannot be a record with Piece less than equal 0 in order items."; 
        public const string THERE_CANNOT_BE_A_RECORD_AMOUNT_LESSTHENEQUAL_ZERO = "There cannot be a record with Amount less than equal 0 in order items."; 
        public const string THERE_CANNOT_BE_A_RECORD_BARCODE_IS_NULL = "There cannot be a record with Barcode is null in order items.";
        public const string NON_RECORD_CANNOT_BE_DELETED = "Non-record cannot be deleted";
        public const string RECORD_NOT_FOUND = "{0} Record not found";
        public const string NOT_VALID_EPOSTA = "not a valid email address";
    }
}
