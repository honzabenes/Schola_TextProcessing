namespace ExcelFramework
{
    /// <summary>
    /// Encapsulates the result of a cell evaluation, containing either a valid value or an error message.
    /// In case of cycle error, it also contains cycle initiator.
    /// </summary>
    public readonly struct EvaluationResult
    {
        public bool IsSuccess { get; }
        public int Value { get; }
        public string ErrorMessage { get; }
        public Cell? CycleInitiatior { get; }

        public EvaluationResult(int value)
        {
            IsSuccess = true;
            Value = value;
            ErrorMessage = string.Empty;
            CycleInitiatior = null;
        }

        public EvaluationResult(string errorMessage, Cell? cycleInitiator = null)
        {
            IsSuccess = false;
            Value = 0;
            ErrorMessage = errorMessage;
            CycleInitiatior = cycleInitiator;
        }
    }
}
