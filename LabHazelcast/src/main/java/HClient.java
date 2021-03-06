import com.hazelcast.aggregation.Aggregators;
import com.hazelcast.client.config.ClientConfig;

import java.io.IOException;
import java.util.Collection;
import java.util.Map;
import java.util.Scanner;
import java.util.UUID;
import java.util.regex.Pattern;

import com.hazelcast.client.HazelcastClient;
import com.hazelcast.core.HazelcastInstance;
import com.hazelcast.map.IMap;
import com.hazelcast.query.Predicate;
import com.hazelcast.query.Predicates;
import sun.awt.im.InputMethodAdapter;

public class HClient {
    public static void main( String[] args ) throws IOException {
        ClientConfig clientConfig = HConfig.getClientConfig();
        HazelcastInstance client = HazelcastClient.newHazelcastClient( clientConfig );

        IMap<UUID, Employee> employeesMap = client.getMap("employees");
        IMap<UUID, Department> departmentsMap = client.getMap("departments");
        GenerateSamples(employeesMap, departmentsMap);

        while (true) {
            Integer choice = printMenu();
            System.out.println(choice);
            if (choice > 0 && choice < 9) {
                switch (choice) {
                    case 1:
                        add(employeesMap, departmentsMap);
                        break;
                    case 2:
                        edit(employeesMap, departmentsMap);
                        break;
                    case 3:
                        getById(employeesMap, departmentsMap);
                        break;
                    case 4:
                        getAll(employeesMap, departmentsMap);
                        break;
                    case 5:
                        remove(employeesMap, departmentsMap);
                        break;
                    case 6:
                        calculateAverageSalary(employeesMap);
                        break;
                    case 7:
                        calculateAverageSalaryClient(employeesMap);
                        break;
                    case 8:
                        getByName(employeesMap, departmentsMap);
                        break;
                }
                System.out.println("Enter aby kontynuowa??");
                System.in.read();
            } else System.out.println("Niepoprawny numer!");
        }
    }

    private static void getByName(IMap<UUID,Employee> employeesMap, IMap<UUID,Department> departmentsMap) {
        Integer s = printSubMenu();
        Scanner scanner = new Scanner(System.in);
        if (s > 0 && s < 3) {
            switch (s) {
                case 1:
                    System.out.println("Wpisz imi?? policjanta:");
                    String employeeName = scanner.next();
                    Predicate predicate = Predicates.equal("name", employeeName);
                    Collection<Employee> employeesCollection = employeesMap.values(predicate);
                    employeesCollection.forEach(player -> System.out.println(player.toString()));
                    break;
                case 2:
                    System.out.println("Wpisz typ komisariatu");
                    String depType = scanner.next();
                    Predicate departmentPredicate = Predicates.equal("type", depType);
                    Collection<Department> depCollection = departmentsMap.values(departmentPredicate);
                    depCollection.forEach(player -> System.out.println(player.toString()));
                    break;
            }
        } else System.out.println("Wrong number, choose again.");
    }

    private static void calculateAverageSalaryClient(IMap<UUID,Employee> employeesMap) {
        double averageSalary = employeesMap.values().stream()
                .mapToDouble(Employee::getSalary)
                .average()
                .orElse(0);
        System.out.println("Srednia pensja policjanta: " + averageSalary);
    }

    private static void calculateAverageSalary(IMap<UUID,Employee> employeesMap) {
        Double averageSalary = employeesMap.aggregate(Aggregators.integerAvg("salary"));
        System.out.println("Srednia pensja policjanta: " + averageSalary);
    }

    private static void remove(IMap<UUID,Employee> employeesMap, IMap<UUID,Department> departmentsMap) {

        Integer s = printSubMenu();
        Scanner scanner = new Scanner(System.in);
        if (s > 0 && s < 3) {
            System.out.println("Podaj klucz:");
            switch (s) {
                case 1:
                    String employeeKey = scanner.next();
                    if (isValidUUID(employeeKey) && employeesMap.containsKey(UUID.fromString(employeeKey))) {
                        Employee player = employeesMap.remove(UUID.fromString(employeeKey));
                        System.out.println("Usuni??to!" + employeeKey + " => " + player.toString());
                    } else System.out.printf("Nie istnieje taki klucz!");
                    break;
                case 2:
                    String departmentKey = scanner.next();
                    if (isValidUUID(departmentKey) && departmentsMap.containsKey(UUID.fromString(departmentKey))) {
                        Department department = departmentsMap.remove(UUID.fromString(departmentKey));
                        System.out.println("Usuni??to!" + departmentKey + " => " + department.toString());
                    } else System.out.printf("Nie istnieje taki klucz!");
                    break;
            }
        } else System.out.println("Niepoprawny numer!");
    }

    private static void getAll(IMap<UUID,Employee> employeesMap, IMap<UUID,Department> departmentsMap) {

        Integer s = printSubMenu();
        if (s > 0 && s < 3) {
            switch (s) {
                case 1:
                    for (Map.Entry<UUID, Employee> e : employeesMap.entrySet()) {
                        System.out.println(e.getKey() + " => " + e.getValue());
                    }
                    break;
                case 2:
                    for (Map.Entry<UUID, Department> e : departmentsMap.entrySet()) {
                        System.out.println(e.getKey() + " => " + e.getValue());
                    }
                    break;
            }
        } else System.out.println("Niepoprawny numer!");
    }

    private static void getById(IMap<UUID,Employee> employeesMap, IMap<UUID,Department> departmentsMap) {

        Integer s = printSubMenu();
        Scanner scanner = new Scanner(System.in);
        if (s > 0 && s < 3) {
            System.out.println("Klucz: ");
            switch (s) {
                case 1:
                    String employeeKey = scanner.next();
                    if (isValidUUID(employeeKey) && employeesMap.containsKey(UUID.fromString(employeeKey))) {
                        Employee employee = employeesMap.get(UUID.fromString(employeeKey));
                        System.out.println(employeeKey + " => " + employee.toString());
                    } else System.out.printf("Nie istnieje taki klucz!");
                    break;
                case 2:
                    String departmentKey = scanner.next();
                    if (isValidUUID(departmentKey) && departmentsMap.containsKey(UUID.fromString(departmentKey))) {
                        Department department = departmentsMap.get(UUID.fromString(departmentKey));
                        System.out.println(departmentKey + " => " + department.toString());
                    } else System.out.printf("Nie istnieje taki klucz!");
                    break;
            }
        } else System.out.println("Niepoprawny numer!");
    }

    private static void edit(IMap<UUID,Employee> employeesMap, IMap<UUID,Department> departmentsMap) {
        Integer s = printSubMenu();
        Scanner scanner = new Scanner(System.in);
        if (s > 0 && s < 3) {
            System.out.println("Id elementu do edycji:");
            switch (s) {
                case 1:
                    String playerId = scanner.next();
                    if (isValidUUID(playerId) && employeesMap.containsKey(UUID.fromString(playerId))) {
                        Employee player = getEmployee(scanner);
                        employeesMap.put(UUID.fromString(playerId), player);
                        System.out.println(playerId + " => " + player.toString());
                    } else System.out.printf("Nie istnieje taki klucz!");
                    break;
                case 2:
                    String clubId = scanner.next();
                    if (isValidUUID(clubId) && departmentsMap.containsKey(UUID.fromString(clubId))) {
                        Department department = getDepartment(scanner);
                        departmentsMap.put(UUID.fromString(clubId), department);
                        System.out.println(clubId + " => " + department.toString());
                    } else System.out.printf("Nie istnieje taki klucz!");
                    break;
            }
        } else System.out.println("Niepoprawny numer!");
    }

    private static Department getDepartment(Scanner scanner) {
        System.out.println("Miasto:");
        String city = scanner.next();
        System.out.println("Typ komisariatu:");
        String type = scanner.next();
        return Department.builder()
                .city(city)
                .type(type)
                .build();
    }


    private final static Pattern UUID_REGEX_PATTERN =
            Pattern.compile("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$");

    public static boolean isValidUUID(String str) {
        if (str == null) {
            return false;
        }
        return UUID_REGEX_PATTERN.matcher(str).matches();
    }
    private static Employee getEmployee(Scanner scanner) {
        System.out.println("Imi??:");
        String name = scanner.next();
        System.out.println("Nazwisko:");
        String surname = scanner.next();
        System.out.println("Rok urodzenia:");
        Integer birthYear = scanner.nextInt();
        System.out.println("Pensja:");
        Double salary = scanner.nextDouble();

        return Employee.builder()
                .name(name)
                .surname(surname)
                .birthYear(birthYear)
                .salary(salary)
                .build();
    }
    private static void add(IMap<UUID,Employee> employeesMap, IMap<UUID,Department> departmentsMap) {
        Integer s = printSubMenu();
        Scanner scanner = new Scanner(System.in);
        if (s > 0 && s < 3) {
            switch (s) {
                case 1:
                    Employee employee = getEmployee(scanner);
                    employeesMap.put(UUID.randomUUID(), employee);
                    break;
                case 2:
                    Department department = getDepartment(scanner);
                    departmentsMap.put(UUID.randomUUID(), department);
                    break;
            }
        } else System.out.println("Nie ma takiego numeru w menu!");
    }

    private static void addEmployee(Employee employee, IMap<UUID,Employee> employeesMap) {
        employeesMap.put(UUID.randomUUID(), employee);
    }

    private static void addDepartment(Department department, IMap<UUID,Department> departmentsMap) {
        departmentsMap.put(UUID.randomUUID(), department);
    }

    private static Integer printMenu(){

        System.out.println(" -- Menu -- ");
        System.out.println( "1 - Dodaj \t" +
                            "2 - Edytuj\n" +
                            "3 - Pobierz przez klucz\t" +
                            "4 - Pobierz wszystko\n" +
                            "5 - Usu??\n" +
                            "6 - Policz ??redni?? pensj?? wg rangi - serwer\n" +
                            "7 - Policz ??redni?? pensj?? wg rangi - klient\n" +
                            "8 - Pobierz przez imi??");
        Scanner scan = new Scanner(System.in);
        return scan.nextInt();
    }
    private static Integer printSubMenu() {
        System.out.println("1 - Pracownicy \t2 - Komisariaty");
        Scanner scan = new Scanner(System.in);
        return scan.nextInt();
    }
    private static void GenerateSamples(IMap<UUID,Employee> employeesMap, IMap<UUID,Department> departmentsMap){
        Employee e1 = Employee.builder().name("Andrzej").surname("Nowak").birthYear(1990).salary(2500).build();
        Employee e2 = Employee.builder().name("Kinga").surname("Kowal").birthYear(1980).salary(3500).build();
        Employee e3 = Employee.builder().name("Bart??omiej").surname("S??omka").birthYear(1997).salary(2500).build();
        Employee e4 = Employee.builder().name("Anna").surname("Pakosz").birthYear(1970).salary(2500).build();

        Department d1 = Department.builder().city("Kielce").type("powiatowy").build();
        Department d2 = Department.builder().city("Kielce").type("wojew??dzki").build();
        Department d3 = Department.builder().city("Radom").type("miejski").build();
        Department d4 = Department.builder().city("Warszawa").type("wojew??dzki").build();

        addEmployee(e1, employeesMap);
        addEmployee(e2, employeesMap);
        addEmployee(e3, employeesMap);
        addEmployee(e4, employeesMap);
        addDepartment(d1, departmentsMap);
        addDepartment(d2, departmentsMap);
        addDepartment(d3, departmentsMap);
        addDepartment(d4, departmentsMap);
    }
}
