CREATE PROCEDURE PobierzHistorieStronnicowane
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        Imie,
        Nazwisko,
        IDGrupy,
        TypAkcji,
        Data
    FROM
        Historia
    ORDER BY
        Data DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

END
