import lombok.*;
import java.io.Serializable;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@ToString

public class Employee implements Serializable {
	private String name;
	private String surname;
	private int birthYear;
	private double salary;
}
